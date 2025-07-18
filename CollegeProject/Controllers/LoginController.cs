using CollegeProject.Models;
using CollegeProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDetailsDto loginDetails)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginRepository.LoginAsync(loginDetails);
                if (result == null) return NotFound();
                else
                    return Ok(result);
            }
            return BadRequest();
        }
    }
}
