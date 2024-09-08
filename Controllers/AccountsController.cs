using Identity1.Data;
using Identity1.Models;
using Identity1.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Versioning;
using System.Net;

namespace Identity1.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountsController(UserManager<ApplicationUser> userManager,ApplicationDbContext Context,SignInManager<ApplicationUser> signInManager,
          RoleManager<IdentityRole> roleManager )
        {
            this.userManager = userManager;
            context = Context;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Gender = model.Gender,
                Address=model.Address,
                UserName = model.Name,
                Email = model.Email,
                PhoneNumber = model.Phone,
            };
            var result=await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(LogIn));
            }
            return View(model);
      }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public IActionResult UsersView()
        {
          var users=userManager.Users.ToList();
            var DisplayUsers = users.Select(soso => new DisplayUsersViewModel()
            {
                City = soso.Address,
                Gender = soso.Gender,
                Name = soso.UserName,
            }); 

            return View(DisplayUsers);
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            IdentityRole Role = new IdentityRole()
            {
                Name=model.RoleName,
            };
            var Result =await roleManager.CreateAsync(Role);
            if (Result.Succeeded)
            {
                return RedirectToAction(nameof(RoleView));
            }
     
            return View(model);
        }

        public IActionResult RoleView()
        {
           var Roles= roleManager.Roles.ToList();
            var DisplayRoles = Roles.Select(aa => new RoleViewModel()
            {
                RoleName = aa.Name
            }).ToList();
            return View(DisplayRoles);
        }
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role=await roleManager.FindByNameAsync(Id);
            var Results =await roleManager.DeleteAsync(role);
            if (Results.Succeeded)
            {
                return RedirectToAction(nameof(RoleView));
            }
            return View();
        
        }
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user =await userManager.FindByNameAsync(Id);
            var Results =await userManager.DeleteAsync(user);
            if (Results.Succeeded)
            {
                return RedirectToAction(nameof(UsersView));
            }
            return View();
        }
        public async Task<IActionResult> UpdateUser(string Id)
        {
            var user = await userManager.FindByNameAsync(Id);
            DisplayUsersViewModel aa = new DisplayUsersViewModel()
                {
                    Name = user.UserName,
                    Gender = user.Gender,
                    City = user.Address,
                    id=user.Id,
                };
                return View(aa);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(DisplayUsersViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.id);

                user.Gender = model.Gender;
                user.Address = model.City;
                user.UserName = model.Name;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UsersView));
                }
            
                return View(model);
             }
    }
    }

