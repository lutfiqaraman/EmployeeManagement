using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Presentation.Models.Roles
{
    public class UserRolesViewModel
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
