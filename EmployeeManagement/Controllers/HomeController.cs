using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController
    {
        private readonly IEmployeeRepository EmployeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }

        public string Index()
        {
            return EmployeeRepository.GetEmployee(1).Name;
        }
    }
}
