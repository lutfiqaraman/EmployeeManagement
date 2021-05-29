using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public Department Department { get; set; }
    }
}
