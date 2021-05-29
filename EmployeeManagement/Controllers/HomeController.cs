using EmployeeManagement.DataAccess.Repositories.Employees;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Presentation.ViewModels;
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
            //IEnumerable<Employee> model = EmployeeRepository.GetAllEmployees();
            //return View(model);
            return View();
        }

        public IActionResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                //Employee = EmployeeRepository.GetEmployee(id ?? 1),
                //PageTitle = "Employee Details"
            };

            //return View(homeDetailsViewModel);
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            //if (ModelState.IsValid)
            //{
            //    Employee newEmployee = EmployeeRepository.CreateEmployee(employee);
            //    return RedirectToAction("details", new { id = newEmployee.Id });
            //}

            return View();
        }
    }
}
