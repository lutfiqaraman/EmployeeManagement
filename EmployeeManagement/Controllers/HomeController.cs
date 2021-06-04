using EmployeeManagement.DataAccess.Repositories.Employees;
using EmployeeManagement.DataAccess.Repositories.Employees.Dto;
using EmployeeManagement.Presentation.Models.Employees;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly IWebHostEnvironment WebHostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            EmployeeRepository = employeeRepository;
            WebHostingEnvironment = webHostEnvironment;
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
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;

                if (model.Photo != null)
                {
                    string imagesFolder = Path.Combine(WebHostingEnvironment.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(imagesFolder, fileName);
                    
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                EmployeeDto newEmployee = new EmployeeDto
                {
                    Name       = model.Name,
                    Email      = model.Email,
                    Department = model.Department,
                    PhotoPath  = fileName
                };

                EmployeeRepository.CreateEmployee(newEmployee);

                return RedirectToAction("Details", new { 
                    id = newEmployee.Id
                });
            }

            return View();
        }
    }
}
