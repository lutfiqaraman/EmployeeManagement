using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.DataAccess.Repositories.Employees.Dto
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}
