using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SULS.Web.Models.Account;

namespace SULS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                return Redirect("/");
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return this.Redirect("/Account/Login");
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser { UserName = model.Username, Email = model.Email, };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return this.Redirect("/Account/Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return this.Redirect("/");
        }


        private async Task createRoles(string[] roleNames)
        {
            foreach (var role in roleNames)
            {
                bool result = await _roleManager.RoleExistsAsync(role);
                if (!result)
                {
                    var newRole = new IdentityRole();
                    newRole.Name = role;
                    await _roleManager.CreateAsync(newRole);
                }
            }
        }
    }
}
