using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Presentation.Controllers
{
    public class AdministrationController : Controller
    {
        public RoleManager<IdentityRole> RoleManager { get; }

        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
