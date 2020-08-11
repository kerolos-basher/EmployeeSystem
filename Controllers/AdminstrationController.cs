using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Idintitycorepro.Data;
using Idintitycorepro.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Idintitycorepro.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminstrationController : Controller
    {
         
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminstrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Role = new IdentityRole()
                {
                    Name = model.Name,
                };
                var result = await roleManager.CreateAsync(Role);
                if (result.Succeeded)
                {
                 
                    return RedirectToAction("ListRoles", "Adminstration");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListUsers()
        {
            var user = userManager.Users;
            return View(user);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var rols = roleManager.Roles;
            return View(rols);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
           var role =  await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Not Founr");
                return RedirectToAction("ListRoles", "Adminstration");
            }
            else
            {
                var model = new EditRoleViewModel()
                {
                    Id = role.Id,
                    Name = role.Name
                };
                foreach (var user in userManager.Users)
                {
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        model.User.Add(user.UserName);
                    }
                }
                return View(model);
            }
           
        }
      
         [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            var role = await roleManager.FindByIdAsync(editRoleViewModel.Id);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Not Founr");
                return RedirectToAction("ListRoles", "Adminstration");
            }
            else
            {

                role.Name = editRoleViewModel.Name;
                var res = await roleManager.UpdateAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Adminstration");
                }
                foreach (var error in res.Errors)
                {
                        ModelState.AddModelError(string.Empty, error.Description);
                       
                }

                return View(editRoleViewModel);
            }
          
        }
        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleid)
        {
            ViewBag.roleid = roleid;
            var role = await roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Not Founr");
                return RedirectToAction("EditRole", "Adminstration");
            }
            else 
            {
                var model = new List<UserRoleViewModel>();
                foreach (var user in userManager.Users)
                {
                    var userinrole = new UserRoleViewModel()
                    { 
                        UserId = user.Id,
                        UserName = user.UserName,
                    };
                    if (await userManager.IsInRoleAsync(user,role.Name))
                    {
                        userinrole.IsSelcted = true;
                    } else
                    {
                        userinrole.IsSelcted = false;
                    }
                    model.Add(userinrole);
                }

                return View(model);

            }

        }
        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model,string roleid)
        {
            var role = await roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Not Founr");
                return RedirectToAction("EditRole", "Adminstration");
            }
            for (int i = 0; i < model.Count; i++)
            { 
               var user =  await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelcted && !(await userManager.IsInRoleAsync(user, role.Name))) 
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (! model[i].IsSelcted && await userManager.IsInRoleAsync(user, role.Name)) 
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                        continue;
                    else
                    {
                        return RedirectToAction("EditRole", new { id = roleid });
                    }
                }
            }
            return RedirectToAction("EditRole", new { id = roleid });
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Not Founr");
                return RedirectToAction("ListUsers", "Adminstration");
            }
            var userCliams = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);
            var model = new EditUesrViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Emil = user.Email,
                City = user.City,
                Cliams = userCliams.Select(c => c.Value).ToList(),
                Roles = userRoles.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUesrViewModel userviewmodel)
        {

            var user = await userManager.FindByIdAsync(userviewmodel.Id);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Not Founr");
                return RedirectToAction("EditUser", "Adminstration");
            }
            if(ModelState.IsValid)
            {
                user.UserName = userviewmodel.UserName;
                user.Email = userviewmodel.Emil;
                user.City = userviewmodel.City;
                var res = await userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Adminstration");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(userviewmodel);
            }
            
            return View(userviewmodel);
        }


        
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "cant Found");
                return RedirectToAction("EditUser", "Adminstration");
            }
              var res =    await userManager.DeleteAsync(user);
            if (res.Succeeded)
            {
                return RedirectToAction("ListUsers", "Adminstration");
            }
            foreach (var error in res.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction("ListUsers", "Adminstration");

        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "cant Found");
                return RedirectToAction("ListRoles", "Adminstration");
            }
            try
            {
                var res = await roleManager.DeleteAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Adminstration");
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return RedirectToAction("ListRoles", "Adminstration");
            }
            catch(DbUpdateException ex)
            {
                ViewBag.ErrorTitle = $"{role.Name} Role Is In Use Please Delete All Youser In The Role First And Then Try Agin"; 
                return View("Error");
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> EditAllUserRoles(string userId)
        {
            ViewBag.roleid = userId;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Not Founr");
                return RedirectToAction("EditUser", "Adminstration");
            }
            else
            {
                var model = new List<ManageAllUserRoles>();
                foreach (var role in roleManager.Roles)
                {
                    var userinrole = new ManageAllUserRoles()
                    {
                        RoleId = role.Id,
                        RoleName = role.Name,
                    };
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        userinrole.IsSelcted = true;
                    }
                    else
                    {
                        userinrole.IsSelcted = false;
                    }
                    model.Add(userinrole);
                }

                return View(model);

            }

        }
        [HttpPost]
        public async Task<IActionResult> EditAllUserRoles(List<ManageAllUserRoles> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Not Founr");
                return RedirectToAction("EditUser", "Adminstration");
            }

            var roles = await userManager.GetRolesAsync(user);
            var res = await userManager.RemoveFromRolesAsync(user, roles);
            if (!res.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "cant remove user from role");
                return RedirectToAction("EditUser", "Adminstration");
            }
            var ress= await userManager.AddToRolesAsync(user,model.Where(x=>x.IsSelcted).Select(c=>c.RoleName));
            if (!ress.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "cant add user to role");
                return RedirectToAction("EditUser", "Adminstration");
            }
            return RedirectToAction("EditUser", new { id = userId });
        }

    }

}

