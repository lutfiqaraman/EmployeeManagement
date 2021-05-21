using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
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

        public ActionResult Details()
        {
            ViewData["PageTitle"] = "Employee Details";

            Employee model = EmployeeRepository.GetEmployee(1);
            return View(model);
        }
    }
}
