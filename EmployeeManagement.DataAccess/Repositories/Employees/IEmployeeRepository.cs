using EmployeeManagement.DataAccess.Repositories.Employees.Dto;
using EmployeeManagement.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        EmployeeDto GetEmployee(int Id);
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDto CreateEmployee(EmployeeDto employee);
        Task UpdateEmployee(EmployeeDto employee);
        Task DeleteEmployee(int Id);
    }
}
