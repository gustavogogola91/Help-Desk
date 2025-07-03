using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using backend.DTO.Erros;
using backend.Exceptions;
using backend.Helpers;
using backend.Interfaces;
using FluentValidation;

namespace backend.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env, IEmailHelper email)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _env = env;
        private readonly IEmailHelper _email = email;

        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

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

                var json = JsonSerializer.Serialize(response, _options);
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

                var json = JsonSerializer.Serialize(response, _options);
                await context.Response.WriteAsync(json);
            }
            catch (InvalidCredentialException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                var response = new APIException(context.Response.StatusCode.ToString(), ex.Message);

                var json = JsonSerializer.Serialize(response, _options);
                await context.Response.WriteAsync(json);
            }
            catch (ArgumentException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = new APIException(context.Response.StatusCode.ToString(), ex.Message);

                var json = JsonSerializer.Serialize(response, _options);
                await context.Response.WriteAsync(json);
            }
            catch (EmailException ex)
            {
                var data = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
                _logger.LogError($"[{string.Format("{0:dd/MM/yyyy-HH-mm-ss}", data)}][Email] Erro ao enviar emails: {ex.InnerException!.Message}");
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ?
                    new APIException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace!.ToString()) :
                    new APIException(context.Response.StatusCode.ToString(), ex.Message, "Internal Server Error");

                if (_env.IsProduction())
                {
                    await _email.SendEmailInternalError(ex);
                }

                 var data = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
                _logger.LogError($"[{string.Format("{0:dd/MM/yyyy-HH-mm-ss}", data)}][Erro] Erro não tratado: {ex.Message}");

                var json = JsonSerializer.Serialize(response, _options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}