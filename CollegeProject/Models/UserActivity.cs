using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeProject.Models
{
    public class UserActivity
    {
        [Key]
        [Column(TypeName ="bigint")]
        public Int64 Id { get; set; }
        [Column(TypeName = "bigint")]
        public Int64 UserId { get; set; }

        public string Token { get; set; }
        public Int64 RoleId { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime CreatedDate { get; set; }  
        
        public Boolean Status { get; set; }
    }
}
