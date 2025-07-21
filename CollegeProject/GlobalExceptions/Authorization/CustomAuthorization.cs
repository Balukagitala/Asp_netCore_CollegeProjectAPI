using CollegeProject.DbContexts;
using CollegeProject.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

public class CustomAuthorizationFilter : IActionFilter
{
    private readonly CollegeDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuthorizationFilter(CollegeDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var httpRequest = context.HttpContext.Request;
        var path = httpRequest.Path.Value.Split('/');
        string controllerName = path.Length > 1 ? path[2] : "";
        string actionName = path.Length > 1 ? path[3] : "";
        string httpMethod = httpRequest.Method;

        var token = httpRequest.Headers["Authorization"].ToString().Replace("Bearer ", "");

        // Get UserData from DB using token
        var user = (from users in _dbContext.Registration
                    join userActivity in _dbContext.UserActivity on users.UserId equals userActivity.UserId
                    join roles in _dbContext.Roles on users.RoleId equals roles.RoleId
                    join mapping in _dbContext.RoleControllerMapping on roles.RoleId equals mapping.RoleId
                    where userActivity.Token == token &&
                          userActivity.Status == true &&
                          mapping.Status == true
                    select new UserData
                    {
                        UserId = users.UserId,
                        UserName = users.UserName,
                        RoleId = roles.RoleId,
                        RoleName = roles.RoleName,
                        ControllerName = mapping.ControllerName,
                        ActionName = mapping.ActionName,
                        IsAllowed = mapping.IsAllowed,
                        IsReadOnly = mapping.IsRedOnly,
                        IsWriteOnly = mapping.IsWriteOnly
                    }).ToList();

        var hasAccess = user.Any(u =>
            u.ControllerName.Equals(controllerName, StringComparison.OrdinalIgnoreCase) &&
            u.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase) &&
            u.IsAllowed &&
            (u.IsReadOnly && httpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase)) ||
            (u.IsWriteOnly &&
            (httpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
             httpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase) ||
             httpMethod.Equals("DELETE", StringComparison.OrdinalIgnoreCase)))
        );

        if (!hasAccess)
        {
            context.Result = new UnauthorizedResult();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Do nothing
    }
}
