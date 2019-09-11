using ChushkaWebApp.Data;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChushkaWebApp.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            this.Db = new ApplicationDbContext();
        }
        public ApplicationDbContext Db { get;}
    }
}
