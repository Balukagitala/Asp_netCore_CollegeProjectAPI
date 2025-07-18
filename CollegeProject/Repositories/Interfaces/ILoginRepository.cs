using CollegeProject.Models;

namespace CollegeProject.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> LoginAsync(LoginDetailsDto  loginDetails);

    }
}
