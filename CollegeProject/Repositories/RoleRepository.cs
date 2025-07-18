using CollegeProject.DbContexts;
using CollegeProject.Models.Roles;
using CollegeProject.Repositories.Interfaces;
using CollegeProject.Responses;
using System.Security.Claims;

namespace CollegeProject.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CollegeDbContext _context;
        public RoleRepository(CollegeDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> CreateRoleAsync(Roles roles)
        {
            roles.CreatedDate = DateTime.Now;
            roles.CreatedBy = 1;
            var roleexists = _context.Roles
                .Count(x => x.RoleName == roles.RoleName && x.Status == true);
            if(roleexists >0)
            {
                return new ApiResponse { Message = "Role Already Exists. Please Check", Success = false };
            }           
            _context.Roles.Add(roles);
            _context.SaveChanges();
            return new ApiResponse { Message = "Role Added Successfully", Success = true };
        }

        public async Task<ApiResponse> CreateRoleControllerMapping(RoleControllerMapping roleControllerMapping)
        {
            var rolemappingCount = _context.RoleControllerMapping
                .Count(x => x.RoleId == roleControllerMapping.RoleId && x.ControllerName == roleControllerMapping.ControllerName
                && x.Status == true && x.ActionName == roleControllerMapping.ActionName);
            if(rolemappingCount>0)
            {
                return new ApiResponse { Message = "Role Mapping Already Exists. Please Check",Success=false };
            }
            _context.RoleControllerMapping.Add(roleControllerMapping);
            _context.SaveChanges();
            return new ApiResponse { Message = "Role Mapping Done Successfully!", Success = false };

        }
    }
}
