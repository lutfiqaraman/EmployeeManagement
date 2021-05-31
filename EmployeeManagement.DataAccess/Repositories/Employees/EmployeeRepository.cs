using AutoMapper;
using EmployeeManagement.DataAccess.EntityFramework;
using EmployeeManagement.DataAccess.Repositories.Employees.Dto;
using EmployeeManagement.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repositories.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext Context;
        private readonly IMapper      Mapper;

        public EmployeeRepository(AppDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper  = mapper;
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
            var employees = Context.Employees;
            List<EmployeeDto> Employees = Mapper.Map<List<EmployeeDto>>(employees);
            
            return Employees;
        }

        public EmployeeDto GetEmployee(int Id)
        {
            Employee employee    = Context.Employees.Find(Id);
            EmployeeDto Employee = Mapper.Map<EmployeeDto>(employee);

            return Employee;
        }

        public Task UpdateEmployee(EmployeeDto employee)
        {
            throw new System.NotImplementedException();
        }
    }
}
