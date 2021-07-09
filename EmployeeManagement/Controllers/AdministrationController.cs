using EmployeeManagement.Presentation.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        public RoleManager<IdentityRole> RoleManager { get; }
        public UserManager<IdentityUser> UserManager { get; }

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await RoleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            IQueryable<IdentityRole> roles = RoleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                return View("Error");
            }

            EditRoleViewModel model = new EditRoleViewModel()
            {
                Id       = role.Id,
                RoleName = role.Name
            };

            foreach (var user in UserManager.Users)
            {
                if (await UserManager.IsInRoleAsync(user, role.Name)) 
                {
                    model.Users.Add(user.UserName);
                };
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                return View("Error");
            }
            else
            {
                role.Name = model.RoleName;
                role.NormalizedName = model.RoleName.ToUpper();

                IdentityResult result = await RoleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        public IActionResult EditUsersInRole()
        {
            return View();
        }
    }
}
