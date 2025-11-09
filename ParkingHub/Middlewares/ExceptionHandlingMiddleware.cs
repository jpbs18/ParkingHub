using FluentValidation;
using ParkingHub.Exceptions;
using System.Net;
using System.Text.Json;

namespace ParkingHub.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            object result;

            switch (exception)
            {
                case NotFoundException:
                    status = HttpStatusCode.NotFound;
                    result = new { error = exception.Message };
                    break;
                case RepositoryException:
                    status = HttpStatusCode.InternalServerError;
                    result = new { error = "Database error occurred." };
                    break;
                case ValidationException validationEx:
                    status = HttpStatusCode.BadRequest;
                    var messages = validationEx.Errors.Select(e => e.ErrorMessage).ToArray();
                    result = new { errors = messages };
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    result = new { error = "An unexpected error occurred." };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var json = JsonSerializer.Serialize(result);
            return context.Response.WriteAsync(json);
        }
    }
}
