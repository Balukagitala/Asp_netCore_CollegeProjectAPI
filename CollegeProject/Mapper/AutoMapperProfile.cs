using AutoMapper;
using CollegeProject.Models;
using CollegeProject.Models.Roles;

namespace CollegeProject.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegistrationDTo, Registration>().ReverseMap();
            CreateMap<RolesDto, Roles>().ReverseMap();
            CreateMap<RoleControllerMappingDto, RoleControllerMapping>().ReverseMap();

        }

    }
}
