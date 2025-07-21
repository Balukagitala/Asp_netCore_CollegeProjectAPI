using System.ComponentModel.DataAnnotations;

namespace CollegeProject.Models
{
    public class RegistrationDTo
    {
        [MinLength(5, ErrorMessage = "Minimum Charcaters should be 5")]
        [MaxLength(50, ErrorMessage = "Maximun Characters should be 50")]
        public string FirstName { get; set; }

        [MinLength(5, ErrorMessage = "Minimum Charcaters should be 5")]
        [MaxLength(50, ErrorMessage = "Maximun Characters should be 50")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MinLength(5, ErrorMessage = "Minimum Charcaters should be 5")]
        [MaxLength(50, ErrorMessage = "Maximun Characters should be 50")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [MinLength(3, ErrorMessage = "Minimum Charcaters should be 1")]
        [MaxLength(20, ErrorMessage = "Maximun Characters should be 20")]
        public string Role { get; set; }

    }
}
