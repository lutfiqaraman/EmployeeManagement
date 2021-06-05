using EmployeeManagement.DataAccess.Repositories.Employees.Dto;
using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Presentation.Models.Employees
{
    public class EmployeeCreateViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}
