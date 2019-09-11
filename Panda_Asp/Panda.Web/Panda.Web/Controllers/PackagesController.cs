using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models;
using Panda.Web.Models.Packages;

namespace Panda.Web.Controllers
{
    public class PackagesController : Controller
    {
        private readonly PandaDbContext _context;

        public PackagesController(PandaDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Packages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Packages
                .Include(p => p.Recipient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (package == null)
            {
                return NotFound();
            }
            string date = "";
            if(package.Status == PackageStatus.Pending )
            {
                date = "N/A";
            }
            else if(package.Status == PackageStatus.Delivered)
            {
                date = "Delivered";
            }

            PackageDetailsViewModel viewModel = new PackageDetailsViewModel
            {
                ShippingAddress = package.ShippingAddress,
                Description = package.Description,
                EstimatedDeliveryDate = date,
                Status = package.Status.ToString(),
                Weight = package.Weight,
                Recipient = _context.Users.FirstOrDefault(a => a.Id == package.RecipientId).UserName
            };
            return View(viewModel);
        }

        // GET: Packages/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            List<string> usernames = this._context.Users.Select(a => a.UserName).ToList();
            var model = new CreatePackageInputModel
            {
                Names = usernames
            };
            ViewData["usernames"] = model.Names;
            return View();
        }

       
        [HttpPost]
        [Authorize(Roles = "Admin")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePackageBindingModel model)
        {

            var package = new Package
            {
                ShippingAddress = model.ShippingAddress,
                Description = model.Description,
                Status = PackageStatus.Pending,
                Weight = model.Weight,
                EstimatedDeliveryDate = null,
                Recipient = this._context.Users.FirstOrDefault(a => a.UserName == model.RecipientName)
            };

            this._context.Packages.Add(package);
            await this._context.SaveChangesAsync();
            return this.Redirect("/");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            List<Package> packagesFromDB = this._context.Packages.Where(a => a.Status == PackageStatus.Pending).ToList();
            var viewModel = new AllPackagesByType();
            foreach (var pack in packagesFromDB)
            {
                PackageByTypeViewModel model = new PackageByTypeViewModel
                {
                    Description = pack.Description,
                    ShippingAddress = pack.ShippingAddress,
                    Recipient = this._context.Users.FirstOrDefault(a => a.Id == pack.RecipientId).UserName,
                    Weight = pack.Weight,
                    Id = pack.Id
                };
                viewModel.Packages.Add(model);
            }
            ViewData["packages"] = viewModel;
            return this.View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delivered()
        {
            List<Package> packagesFromDB = this._context.Packages.Where(a => a.Status == PackageStatus.Delivered).ToList();
            var viewModel = new AllPackagesByType();
            foreach (var pack in packagesFromDB)
            {
                PackageByTypeViewModel model = new PackageByTypeViewModel
                {
                    Description = pack.Description,
                    ShippingAddress = pack.ShippingAddress,
                    Recipient = this._context.Users.FirstOrDefault(a => a.Id == pack.RecipientId).UserName,
                    Weight = pack.Weight,
                    Id = pack.Id
                };
                viewModel.Packages.Add(model);
            }
            ViewData["packages"] = viewModel;
            return this.View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deliver(string id)
        {
            if (id == null)
            {
                if (id == null)
                {
                    return NotFound();
                }
            }

            var package = this._context.Packages.FirstOrDefault(a => a.Id == id);
            package.Status = PackageStatus.Delivered;

            this._context.Packages.Update(package);
            await this._context.SaveChangesAsync();
            return this.Redirect("/");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Ship(string id)
        {
            if (id == null)
            {
                if (id == null)
                {
                    return NotFound();
                }
            }

            var package = this._context.Packages.FirstOrDefault(a => a.Id == id);
            package.Status = PackageStatus.Shipped;
            
            package.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(RandomDays());
            this._context.Packages.Update(package);
            await this._context.SaveChangesAsync();
            return this.Redirect("/");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Shipped()
        {
            List<Package> packagesFromDB = this._context.Packages.Where(a => a.Status == PackageStatus.Shipped).ToList();
            var viewModel = new AllPackagesByType();
            foreach (var pack in packagesFromDB)
            {
                PackageByTypeViewModel model = new PackageByTypeViewModel
                {
                    Description = pack.Description,
                    ShippingAddress = pack.ShippingAddress,
                    Recipient = this._context.Users.FirstOrDefault(a => a.Id == pack.RecipientId).UserName,
                    Weight = pack.Weight,
                    Id = pack.Id
                };
                viewModel.Packages.Add(model);
            }
            ViewData["packages"] = viewModel;
            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Acquire(string id)
        {
            if (id == null)
            {
                if (id == null)
                {
                    return NotFound();
                }
            }
            var package = this._context.Packages.FirstOrDefault(a => a.Id == id);
            package.Status = PackageStatus.Acquired;
            this._context.Packages.Update(package);
            Receipt receipt = new Receipt
            {
                Fee = package.Weight * 2.67M,
                IssuedOn = DateTime.UtcNow,
                PackageId = package.Id,
                RecipientId = package.RecipientId,
                Package = package,
                Recipient = this._context.Users.FirstOrDefault(a => a.Id == package.RecipientId)
            };
            this._context.Receipts.Add(receipt);
            await this._context.SaveChangesAsync();
            return this.Redirect("/");
        }

        private int RandomDays()
        {
            Random r = new Random();
            int result = r.Next(20, 40);

            return result;
        }
    }
}
