using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> employeeList;

        public MockEmployeeRepository()
        {
            employeeList = new List<Employee>
            {
                new Employee() { Id = 1, Name = "Mary" , Department = Dept.HR,  Email = "mary@mockemp.com" },
                new Employee() { Id = 2, Name = "Jade" , Department = Dept.IT,  Email = "jade@mockemp.com" },
                new Employee() { Id = 3, Name = "John" , Department = Dept.IT,  Email = "john@mockemp.com" },
                new Employee() { Id = 4, Name = "Ryan" , Department = Dept.Financial,  Email = "ryan@mockemp.com" }
            };
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
