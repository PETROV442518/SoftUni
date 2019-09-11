using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.Data;
using Panda.Models;
using Panda.Services;
using Panda.Services.Dtos;
using Panda.Web.Models;
using Panda.Web.Models.Packages;

namespace Panda.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PandaDbContext _context;
        private readonly IPackagesServices _packagesServices;
        private readonly SignInManager<PandaUser> _signInManager;
     
        private readonly UserManager<PandaUser> userManager;

        public HomeController(IPackagesServices packagesServices, SignInManager<PandaUser> signInManager, PandaDbContext context, UserManager<PandaUser> userManager)
        {
            this._context = context;
            this._packagesServices = packagesServices;
            this._signInManager = signInManager;
            
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            if(this._signInManager.IsSignedIn(User))
            {
                List<PackageIndexInfo> pendingPackages = _packagesServices.GetPackagesByStatus(PackageStatus.Pending)
                    .Where(a => a.RecipientId == userManager.GetUserId(User)).ToList();
                  
                List<PackageIndexInfo> shippedPackages = _packagesServices.GetPackagesByStatus(PackageStatus.Shipped)
                 .Where(a => a.RecipientId == userManager.GetUserId(User)).ToList();

                List<PackageIndexInfo> delieredPackages = _packagesServices.GetPackagesByStatus(PackageStatus.Delivered)
                    .Where(a => a.RecipientId == userManager.GetUserId(User)).ToList();

                IndexAllPackagesViewModel viewModel = new IndexAllPackagesViewModel
                {
                    Delivered = delieredPackages,
                    Shipped = shippedPackages,
                    Pending = pendingPackages,
                };
                return View(viewModel);
            }
            
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
