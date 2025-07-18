using CollegeProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {

        //[HttpPost]
        //[Route("UpdateProfile")]
        //public async Task<IActionResult> UpdateProfile([FromBody] Registration registrationDTo)
        //{

        //}
    }
}
