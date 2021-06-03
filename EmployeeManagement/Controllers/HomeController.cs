using EmployeeManagement.DataAccess.Repositories.Employees;
using EmployeeManagement.DataAccess.Repositories.Employees.Dto;
using EmployeeManagement.Presentation.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository EmployeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            IReadOnlyList<EmployeeDto> employees = 
                (IReadOnlyList<EmployeeDto>) EmployeeRepository.GetAllEmployees();

            var model = new EmployeeListViewModel 
            {
                Employees = employees
            };

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                Employee = EmployeeRepository.GetEmployee(id ?? 1),
            };

            return View(employeeViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeDto newEmployee = EmployeeRepository.CreateEmployee(employee);

                return RedirectToAction("Details", new { 
                    id = newEmployee.Id
                });
            }

            return View();
        }
    }
}
