using EmployeeManagement.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> employeeList;

        public MockEmployeeRepository()
        {
            employeeList = new List<Employee>
            {
                new Employee() { Id = 1, Name = "Mary" , Department = Department.HR,  Email = "mary@mockemp.com" },
                new Employee() { Id = 2, Name = "Jade" , Department = Department.IT,  Email = "jade@mockemp.com" },
                new Employee() { Id = 3, Name = "John" , Department = Department.IT,  Email = "john@mockemp.com" },
                new Employee() { Id = 4, Name = "Ryan" , Department = Department.Financial,  Email = "ryan@mockemp.com" }
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = employeeList.Max(e => e.Id) + 1;
            employeeList.Add(employee);

            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
