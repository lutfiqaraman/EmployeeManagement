using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Presentation.Models.Users
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles  = new List<string>();
        }

        public string Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        public List<string> Claims { get; set; }
        
        public IList<string> Roles { get; set; }
    }
}
