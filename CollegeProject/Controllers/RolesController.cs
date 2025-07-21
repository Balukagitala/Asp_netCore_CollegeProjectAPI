using AutoMapper;
using CollegeProject.Models.Roles;
using CollegeProject.Repositories.Interfaces;
using CollegeProject.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;
        private readonly IMapper _mapper;

        public RolesController(IRoleRepository roleRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("CreateRole")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> CreateRole(RolesDto rolesDto)
        {
            if (ModelState.IsValid)
            {
                var rolesmodel = _mapper.Map<Roles>(rolesDto);
                var result = await roleRepository.CreateRoleAsync(rolesmodel);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateRoleMapping")]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> CreateRoleMapping(RoleControllerMappingDto controllerMappingDto)
        {
            if (ModelState.IsValid)
            {
                var roleMappingModel = _mapper.Map<RoleControllerMapping>(controllerMappingDto);
                var result = roleRepository.CreateRoleControllerMapping(roleMappingModel);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
