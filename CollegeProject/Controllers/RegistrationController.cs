using AutoMapper;
using CollegeProject.Models;
using CollegeProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IMapper _mapper;
        public RegistrationController(IRegistrationRepository registrationRepository,IMapper mapper)
        {
            _registrationRepository = registrationRepository;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationDTo registrationDTo)
        {
            var registrationdomain = _mapper.Map<Registration>(registrationDTo);
            var result = await _registrationRepository.CreateUserAsync(registrationdomain);
            if (result == null)
            {
                return BadRequest("Registration Failed.");
            }
            return Ok("Registration Completed Successfully. Please Login");
        }
    }
}
