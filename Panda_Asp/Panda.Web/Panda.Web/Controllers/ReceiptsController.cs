using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models;
using Panda.Web.Models.Receipts;

namespace Panda.Web.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly PandaDbContext _context;
        private readonly UserManager<PandaUser> _userManager;

        public ReceiptsController(PandaDbContext context, UserManager<PandaUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        [Authorize]
        public IActionResult All()
        {
            List<Receipt> receipts = this._context.Receipts
                .Where(a => a.RecipientId == _userManager.GetUserId(User))
                .Include(a => a.Package)
                .Include(a => a.Recipient)
                .ToList();

            ReceiptsAllViewModel viewModel = new ReceiptsAllViewModel();
            foreach (var r in receipts)
            {
                ReceiptDetailsViewModel model = new ReceiptDetailsViewModel
                {
                    Fee = r.Fee,
                    Id = r.Id,
                    ShippingAddress = r.Package.ShippingAddress,
                    Description = r.Package.ShippingAddress,
                    PackageWeight = r.Package.Weight,
                    IssuedOn = r.IssuedOn,
                    RecipientName = r.Recipient.UserName
                };
                viewModel.Receipts.Add(model);
            }
            return View(viewModel);
        }
        [Authorize]
        public IActionResult Details(string id)
        {
            Receipt receipt = this._context.Receipts
                .Include(a => a.Package)
                .Include(a => a.Recipient)
                .FirstOrDefault(a => a.Id == id);
            ReceiptDetailsViewModel viewModel = new ReceiptDetailsViewModel
            {
                Id = receipt.Id,
                IssuedOn = receipt.IssuedOn,
                ShippingAddress = receipt.Package.ShippingAddress,
                Description = receipt.Package.Description,
                Fee = receipt.Fee,
                PackageWeight = receipt.Package.Weight,
                RecipientName = receipt.Recipient.UserName
            };
            return this.View(viewModel);
        }


    }
}