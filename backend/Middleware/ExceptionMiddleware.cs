using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using backend.DTO.Erros;
using backend.Exceptions;
using backend.Helpers;
using FluentValidation;

namespace backend.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _env = env;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                var response = new APIException(context.Response.StatusCode.ToString(), ex.Message, ex.Id != null ? $"Id: {ex.Id}" : "Não informado");

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var errosDict = new Dictionary<string, string[]>();
                foreach (var erro in ex.Errors)
                {
                    if (!errosDict.ContainsKey(erro.PropertyName))
                    {
                        errosDict[erro.PropertyName] = [erro.ErrorMessage];
                    }
                    else
                    {
                        errosDict[erro.PropertyName] = errosDict[erro.PropertyName].Append(erro.ErrorMessage).ToArray();
                    }
                }

                var erroString = ValidationErrorFormater.FormatErrorsToString(errosDict);

                var response = new APIException(context.Response.StatusCode.ToString(), ex.Message, erroString);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
            catch (InvalidCredentialException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                _logger.LogWarning(ex, ex.Message);

                var response = new APIException(context.Response.StatusCode.ToString(), ex.Message);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
            catch (ArgumentException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = new APIException(context.Response.StatusCode.ToString(), ex.Message);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ?
                    new APIException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace!.ToString()) :
                    new APIException(context.Response.StatusCode.ToString(), ex.Message, "Internal Server Error");

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}