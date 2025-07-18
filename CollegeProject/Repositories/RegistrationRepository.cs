using CollegeProject.DbContexts;
using CollegeProject.Models;
using CollegeProject.Repositories.Interfaces;

namespace CollegeProject.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly CollegeDbContext _context;

        public RegistrationRepository(CollegeDbContext context)
        {
            _context = context;
        }

        public async Task<Registration> CreateUserAsync(Registration registration)
        {
            var registrationDomain = new Registration
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Email = registration.Email,
                UserName = registration.UserName,
                Password = registration.Password,
                PhoneNumber = registration.PhoneNumber,
                RoleId = registration.RoleId,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = null,
                ModifiedDate = null,
                CreatedBy = null
            };
            var ValidateUser = _context.Registration.FirstOrDefault(x =>
            (x.UserName == registrationDomain.UserName && x.LastName == registrationDomain.LastName) || (x.UserName == registrationDomain.UserName));
            if (ValidateUser == null) { 
                await _context.Registration.AddAsync(registrationDomain);
            await _context.SaveChangesAsync();
            return registration;
            }
            return null;

        }
    }
}
