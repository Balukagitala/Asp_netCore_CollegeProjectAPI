using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeProject.Models
{
    public class Users
    {
        [Key]
        public Int64 UserId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        //[MinLength(1,ErrorMessage="Minimum Charcaters should be 1")]
        //[MaxLength(1,ErrorMessage="Maximun Characters should be 50")]
        public string FirstName { get; set; }


        [Required]
        [Column(TypeName = "Varchar(50)")]
        //[MinLength(1, ErrorMessage = "Minimum Charcaters should be 1")]
        //[MaxLength(1, ErrorMessage = "Maximun Characters should be 50")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        //[MinLength(1, ErrorMessage = "Minimum Charcaters should be 1")]
        //[MaxLength(1, ErrorMessage = "Maximun Characters should be 20")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "Varchar(50)")]
        //[DataType(DataType.Password)] 
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "Varchar(20)")]
        //[DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "Varchar(20)")]
        public string Role { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Int64 CreatedBy { get; set; } 

        public DateTime ModifiedDate { get; set; }
        public Int64 ModifiedBy { get; set; }
    }
}
