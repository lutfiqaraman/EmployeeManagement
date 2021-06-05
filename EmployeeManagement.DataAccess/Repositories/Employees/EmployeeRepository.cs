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

        public CreateEditEmployeeDto CreateEmployee(CreateEditEmployeeDto employee)
        {
            Employee Employee = Mapper.Map<Employee>(employee);
            
            Context.Employees.Add(Employee);
            Context.SaveChanges();

            employee.Id = Employee.Id;

            return employee;
        }

        public async Task DeleteEmployee(int Id)
        {
            Employee employee = Context.Employees.Find(Id);

            if (employee != null)
            {
                Context.Employees.Remove(employee);
                await Context.SaveChangesAsync();
            }
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

        public CreateEditEmployeeDto UpdateEmployee(CreateEditEmployeeDto employee)
        {
            Employee Employee = Mapper.Map<Employee>(employee);

            var updateEmployee = Context.Employees.Attach(Employee);
            updateEmployee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            Context.SaveChanges();

            return employee;
        }
    }
}
