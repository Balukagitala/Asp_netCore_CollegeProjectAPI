using CollegeProject.Models.Roles;
using CollegeProject.Responses;

namespace CollegeProject.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<ApiResponse> CreateRoleAsync(Roles roles);
        Task<ApiResponse> CreateRoleControllerMapping(RoleControllerMapping roleControllerMapping);
    }
}
