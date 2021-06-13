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
            EmployeeRepository    = employeeRepository;
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
            throw new Exception("exception is here ...");
            EmployeeDto employee = EmployeeRepository.GetEmployee(id.Value);
            
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }

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
                string fileName = ProcessUploadedFile(model);

                CreateEditEmployeeDto newEmployee = new CreateEditEmployeeDto
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            EmployeeDto model = EmployeeRepository.GetEmployee(id);

            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id                    = model.Id,
                Name                  = model.Name,
                Email                 = model.Email,
                Department            = model.Department,
                ExistingEmployeePhoto = model.PhotoPath
            };

            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                CreateEditEmployeeDto updateEmployee = new CreateEditEmployeeDto();

                updateEmployee.Id = model.Id;
                updateEmployee.Name = model.Name;
                updateEmployee.Email = model.Email;
                updateEmployee.Department = model.Department;

                if (model.Photo != null)
                {
                    if (model.ExistingEmployeePhoto != null)
                    {
                        string filePath =
                            Path.Combine(WebHostingEnvironment.WebRootPath,
                                "images", model.ExistingEmployeePhoto);

                        System.IO.File.Delete(filePath);
                    }

                    updateEmployee.PhotoPath = ProcessUploadedFile(model);
                }
                else
                {
                    updateEmployee.PhotoPath = model.ExistingEmployeePhoto;
                }

                EmployeeRepository.UpdateEmployee(updateEmployee);

                return RedirectToAction("Index");
            }

            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string fileName = string.Empty;

            if (model.Photo != null)
            {
                string imagesFolder = Path.Combine(WebHostingEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(imagesFolder, fileName);

                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
