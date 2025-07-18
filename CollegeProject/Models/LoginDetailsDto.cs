using System.ComponentModel.DataAnnotations;

namespace CollegeProject.Models
{
    public class LoginDetailsDto
    {
       
        [MinLength(1,ErrorMessage ="Minimum Length of the UserName is 1 Character")]
        [MaxLength(10, ErrorMessage = "Maximum Length of the UserName is 10 Character")]
        public string? UserName { get; set; }
        [DataType(DataType.Password)]
        [MinLength(1, ErrorMessage = "Minimum Length of the UserName is 1 Character")]
        public string?Password { get; set; }

    }
}
