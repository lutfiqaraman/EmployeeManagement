using EmployeeManagement.Presentation.Models.Claims;
using EmployeeManagement.Presentation.Models.Roles;
using EmployeeManagement.Presentation.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
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
            string decryptId = Encryption.Decrypt(id);
            IdentityRole role = await RoleManager.FindByIdAsync(decryptId);

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

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                return View("Error");
            }
            else
            {

                try
                {
                    IdentityResult result = await RoleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("ListRoles");
                }
                catch (Exception)
                {
                    ViewBag.ErrorMessage = $"{role.Name} is in use, please empty the role then delete it ... ";
                    return View("Error");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            string decryptedID = Encryption.Decrypt(roleId);
            ViewBag.roleId = decryptedID;
            IdentityRole role = await RoleManager.FindByIdAsync(decryptedID);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            List<UserRoleViewModal> model = new List<UserRoleViewModal>();

            foreach (var user in UserManager.Users)
            {
                UserRoleViewModal UserRoleViewModal = new UserRoleViewModal()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await UserManager.IsInRoleAsync(user, role.Name))
                {
                    UserRoleViewModal.IsSelected = true;
                } 
                else
                {
                    UserRoleViewModal.IsSelected = false;
                }

                model.Add(UserRoleViewModal);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModal> model, string roleId)
        {
            string decryptedID = Encryption.Decrypt(roleId);
            IdentityRole role = await RoleManager.FindByIdAsync(decryptedID);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                IdentityUser user = await UserManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                bool isInRole = await UserManager.IsInRoleAsync(user, role.Name);

                if(model[i].IsSelected && !isInRole)
                {
                    result = await UserManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && isInRole)
                {
                    result = await UserManager.RemoveFromRoleAsync(user, role.Name);
                } 
                else
                {
                    continue;
                }

                if(result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            IQueryable<IdentityUser> users = UserManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            string decryptId = Encryption.Decrypt(id);
            IdentityUser user = await UserManager.FindByIdAsync(decryptId);

            if (user == null)
            {
                return View("Error");
            }

            IList<Claim>  userClaims = await UserManager.GetClaimsAsync(user);
            IList<string> userRoles  = await UserManager.GetRolesAsync(user);

            EditUserViewModel model = new EditUserViewModel()
            {
                Id       = user.Id,
                Email    = user.Email,
                UserName = user.UserName,
                Claims   = userClaims.Select(x => x.Value).ToList(),
                Roles    = userRoles

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            IdentityUser user = await UserManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return View("Error");
            } 
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;

                IdentityResult result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            IdentityUser user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return View("Error");
            }
            else
            {
                IdentityResult result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
            }

            return View("ListUsers");
        }

        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            IdentityUser user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach(var role in RoleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await UserManager.IsInRoleAsync(user, role.Name))
                    userRolesViewModel.IsSelected = true;
                else
                    userRolesViewModel.IsSelected = false;

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var roles = await UserManager.GetRolesAsync(user);
            var result = await UserManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user from existing roles");
                return View(model);
            }

            result = await UserManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            IList<Claim> existingUserClaims = await UserManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel()
            {
                UserId = userId
            };

            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }

                model.Claims.Add(userClaim);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                return View("NotFound");
            }

            var claims = await UserManager.GetClaimsAsync(user);
            var result = await UserManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Cannot remove user existing claims");
                return View(model);
            }

            result = await UserManager.AddClaimsAsync(user,
                model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Cannot add selected claims");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = model.UserId });
        }

    }
}
