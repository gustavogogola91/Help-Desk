using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using backend.DTO.Erros;
using backend.Exceptions;
using backend.Helpers;
using backend.Interfaces;
using FluentValidation;

namespace backend.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env, IServiceScopeFactory serviceScope, IConfiguration config, IFileLoggerHelper fileLogger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _env = env;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScope;
        private readonly IConfiguration _config = config;
        private readonly IFileLoggerHelper _fileLogger = fileLogger;

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
                var logMessage = $"[{string.Format("{0:dd/MM/yyyy-HH-mm-ss}", data)}][Email] Erro ao enviar emails: {ex.InnerException!.Message} - {ex.InnerException!.StackTrace}";

                _logger.LogError(logMessage);
                _fileLogger.LogEmailError(logMessage);
            }
            catch (InvalidConfiguratioException ex)
            {
                var data = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
                var logMessage = $"[{string.Format("{0:dd/MM/yyyy-HH-mm-ss}", data)}][Configuração] Erro de configuração: {ex.Message} - {ex.StackTrace}";

                if (_env.IsDevelopment())
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var emailHelper = scope.ServiceProvider.GetRequiredService<IEmailHelper>();
                        await emailHelper.SendEmailInternalError(ex);
                    }
                }

                _logger.LogError(logMessage);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment() ?
                    new APIException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace!.ToString()) :
                    new APIException(context.Response.StatusCode.ToString(), ex.Message, "Internal Server Error");

                if (_env.IsDevelopment())
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var emailHelper = scope.ServiceProvider.GetRequiredService<IEmailHelper>();
                        await emailHelper.SendEmailInternalError(ex);
                    }
                }

                var data = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
                var logMessage = $"[{string.Format("{0:dd/MM/yyyy-HH:mm:ss}", data)}][Erro] Erro não tratado: {ex.Message} - {ex.StackTrace}";

                _fileLogger.LogError(logMessage);
                _logger.LogError(logMessage);

                var json = JsonSerializer.Serialize(response, _options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}