using CollegeProject.Models;

namespace CollegeProject.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
         Task<Registration> CreateUserAsync(Registration registration);
    }
}
