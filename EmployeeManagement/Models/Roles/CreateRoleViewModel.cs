using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Presentation.Models.Roles
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
