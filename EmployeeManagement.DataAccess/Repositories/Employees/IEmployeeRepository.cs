using EmployeeManagement.DataAccess.Repositories.Employees.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        EmployeeDto GetEmployee(int Id);
        IEnumerable<EmployeeDto> GetAllEmployees();
        Task CreateEmployee(EmployeeDto employee);
        Task UpdateEmployee(EmployeeDto employee);
        Task DeleteEmployee(int Id);
    }
}
