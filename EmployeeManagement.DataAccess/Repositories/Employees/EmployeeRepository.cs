using EmployeeManagement.DataAccess.EntityFramework;
using EmployeeManagement.DataAccess.Repositories.Employees.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repositories.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext Context;

        public EmployeeRepository(AppDbContext context)
        {
            Context = context;
        }

        public Task CreateEmployee(EmployeeDto employee)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteEmployee(int Id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            throw new System.NotImplementedException();
        }

        public EmployeeDto GetEmployee(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateEmployee(EmployeeDto employee)
        {
            throw new System.NotImplementedException();
        }
    }
}
