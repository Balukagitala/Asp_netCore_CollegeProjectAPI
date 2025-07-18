using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeProject.Models.Roles
{
    public class Roles
    {
        [Key]
        [Column(TypeName ="bigint")]
        public long RoleId { get; set; }

        [Column(TypeName ="varchar(150)")]
        public string RoleName { get; set; }
        public Boolean Status { get; set; } 
        public DateTime CreatedDate { get; set; }
        public Int64 CreatedBy { get; set; }
    }
}
