using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeProject.Models.Roles
{
    public class RoleControllerMappingDto
    {
        public long RoleId { get; set; }        
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public Boolean IsAllowed { get; set; }
        public Boolean Status { get; set; }

    }
}
