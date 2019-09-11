using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SULS.Services;
using SULS.Web.Models;
using SULS.Web.Models.Home;

namespace SULS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemServices _problemServices;

        public HomeController(IProblemServices problemServices)
        {
            this._problemServices = problemServices;
        }
        public IActionResult Index()
        {
            var viewModel = new AllProblemsViewModel { Problems = this._problemServices.GetAllProblems() };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
