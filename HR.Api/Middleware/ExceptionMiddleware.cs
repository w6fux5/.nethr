using HR.Api.Models;
using HR.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace HR.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(httpContext, ex);

            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetials problem = new();

            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;

                    problem = new CustomProblemDetials
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadHttpRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;

                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomProblemDetials
                    {
                        Title = notFoundException.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFoundException),
                        Detail = notFoundException.InnerException?.Message,
                    };
                    break;

                default:
                    problem = new CustomProblemDetials
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;

            var logMessage = JsonConvert.SerializeObject(problem);
            _logger.LogError(ex, logMessage);
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
