using CollegeProject.DbContexts;
using CollegeProject.Models.Roles;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace CollegeProject.GlobalExceptions
{
    public class TokenValidatingMiddleware
    {
        private readonly RequestDelegate _next;
        // private readonly CollegeDbContext collegeDbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public TokenValidatingMiddleware(RequestDelegate next,IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var path = httpContext.Request.Path.Value;
            if (path != null && (path.Contains("Login", StringComparison.OrdinalIgnoreCase)
                      || path.Contains("registration", StringComparison.OrdinalIgnoreCase)))
            {
                await _next(httpContext);
                return;
            }
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);
                    var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var collegeDbContext = scope.ServiceProvider.GetRequiredService<CollegeDbContext>();
                        var isValid = collegeDbContext.UserActivity
                                      .Any(x => x.Token == token && x.Status == true && x.ExpiryDate > DateTime.Now);

                        if (!isValid)
                        {
                            httpContext.Response.StatusCode = 401;
                            await httpContext.Response.WriteAsJsonAsync(new { Message = "Token is invalid or expired", StatusCode = 401 });
                            return;
                        }  
                    }
                    await _next(httpContext);

                }
                catch (Exception ex)
                {
                    httpContext.Response.StatusCode = 401; // Unauthorized
                    httpContext.Response.ContentType = "application/json";
                    var response = new
                    {
                        Message = "Invalid Token",
                        StatusCode = 401
                    };
                    await httpContext.Response.WriteAsJsonAsync(response);
                    return;
                }
            }
        }
    }
}
