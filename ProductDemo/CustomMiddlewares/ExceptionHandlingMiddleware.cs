using ProductDemo.ViewModels;
using System.Net;
using System.Text.Json;

namespace ProductDemo.CustomMiddlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new ErrorResponse();
            switch (exception)
            {
                case ApplicationException ex:
                    //if (ex.Message.Contains("Invalid Token"))
                    //{
                    //    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    //    errorResponse.ResponseMessage = ex.Message;
                    //    break;
                    //}
                    errorResponse.ResponseMessage = ex.Message;
                    errorResponse.ResponseCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:                    
                    errorResponse.ResponseMessage = "Internal Server Error!";
                    errorResponse.ResponseCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            _logger.LogError(exception.Message);
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
