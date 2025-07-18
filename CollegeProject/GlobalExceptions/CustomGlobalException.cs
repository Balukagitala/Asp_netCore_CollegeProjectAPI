using CollegeProject.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace CollegeProject.GlobalExceptions
{
    public class CustomGlobalException 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomGlobalException> _logger;

        public CustomGlobalException(RequestDelegate requestDelegate, ILogger<CustomGlobalException> logger)
        {
            _next = requestDelegate;
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
                _logger.LogError(ex.Message, "Please contact Administrator");
                _logger.LogError(ex.StackTrace);
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";

                var response = new ErroResponse
                {
                    Message = "Please Contact Administrator",
                    StatusCode = 500
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
