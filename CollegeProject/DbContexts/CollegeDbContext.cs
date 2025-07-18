using CollegeProject.Models;
using CollegeProject.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace CollegeProject.DbContexts
{
    public class CollegeDbContext: DbContext
    {
        public CollegeDbContext(DbContextOptions options):base(options) { }

        public DbSet<Registration> Registration { get; set; }
        public DbSet<UserActivity> UserActivity { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RoleControllerMapping> RoleControllerMapping { get; set; }


    }
}
