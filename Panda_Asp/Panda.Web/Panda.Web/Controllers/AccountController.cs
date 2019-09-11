using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.Models;
using Panda.Web.Models.Account;
using System.Threading.Tasks;

namespace Panda.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<PandaUser> _signInManager;
        private readonly UserManager<PandaUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<PandaUser> signInManager, UserManager<PandaUser> userManager, RoleManager<IdentityRole> roleManager)
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
        public async Task<IActionResult> Login(UsersLoginInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username,
                   model.Password, false, false);

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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new PandaUser { UserName = model.Username, Email = model.Email, };

                var result = await _userManager.CreateAsync(user, model.Password);

                await createRoles(new string[] { "Admin", "User" });

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await this._signInManager.SignInAsync(user, false);
                    return Redirect("/");
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