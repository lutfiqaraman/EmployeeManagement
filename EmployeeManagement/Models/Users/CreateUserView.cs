using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Presentation.Models.Users
{
    public class CreateUserView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
