using EmployeeManagement.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.DataAccess.Repositories.Employees.Dto
{
    public class CreateEditEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
