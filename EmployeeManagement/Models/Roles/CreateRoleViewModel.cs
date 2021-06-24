using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Presentation.Models.Roles
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
