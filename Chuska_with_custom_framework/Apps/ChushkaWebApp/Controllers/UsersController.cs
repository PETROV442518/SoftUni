using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChushkaWebApp.Controllers
{
    public class UsersController: BaseController
    {
        private readonly IHashService hashService;

        public UsersController(IHashService hashService)
        {
            this.hashService = hashService;
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            if(!this.Request.Cookies.ContainsCookie(".auth-cakes"))
            {
                return this.Redirect("/");
            }
            var cookie = this.Request.Cookies.GetCookie(".auth-cakes");
            cookie.Delete();
            this.Response.Cookies.Add(cookie);
            return this.Redirect("/");
        }
        public IHttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public IHttpResponse Login(DoLoginInputModel model)
        {
            var hashedPassword = this.hashService.Hash(model.Password);
            var user = this.Db.Users.FirstOrDefault(x => x.Username == model.Username.Trim() &&
            x.Password = hashedPassword);
            if(user == null)
            {
                return this.BadRequestErrorWithView("Invalid username or password!");
            }
            var mvcUser = new MvcUserInfo
            {
                Username = user.Username,
                Role = user.Role.ToString(),
                Info = user.FullName
                
            };
            var cookieContent = this.UserCookieService.GetUserCookie
        }
    }
}
