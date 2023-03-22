using OrdersApp.Shared.DTO;

namespace OrdersApp.Server.Servises
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ILogger<ExceptionLoggingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Message: {ex.Message}");
                var response = new ErrorResponse
                {
                    Message = ex.Message,
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
