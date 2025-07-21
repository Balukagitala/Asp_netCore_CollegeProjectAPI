using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeProject.Models.Roles
{
    public class RoleControllerMapping
    {
        [Key]
        [Column(TypeName ="bigint")]
        public Int64 Id { get; set; }

        [Column(TypeName ="varchar(250)")]
        public string ControllerName { get; set; }

        [Column(TypeName ="varchar(150)")]
        public string ActionName { get; set; }

        [Column(TypeName ="bit")]
        public Boolean IsAllowed { get; set; }
        [Column(TypeName = "bit")]
        public Boolean Status {  get; set; }

        public Boolean IsRedOnly { get; set; }

        public Boolean IsWriteOnly { get; set; }
        public long RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Roles Roles { get; set; }

    }
}
