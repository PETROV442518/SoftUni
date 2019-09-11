using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SULS.Data;
using SULS.Domain;
using SULS.Web.Models.Submissions;

namespace SULS.Web.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly SULSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SubmissionController(SULSDbContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }
        public IActionResult Create(string id)
        {
            Problem problem = _context.Problems.FirstOrDefault(a => a.Id == id);
            var viewModel = new SubmissionCreateViewModel
            {
                ProblemId = problem.Id,
                ProblemName = problem.Name
            };
            return View(viewModel);
        }


        public async Task<IActionResult> Delete(string id)
        {
            Submission sub = _context.Submissions.FirstOrDefault(a => a.Id == id);
            _context.Submissions.Remove(sub);
            await _context.SaveChangesAsync();
            return Redirect("/");
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubmissionCreateBindingModel model)

        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Create Submission attempt");
                return this.Redirect("/");
            }

            Problem problem = _context.Problems.FirstOrDefault(a => a.Id == model.ProblemId);
            Submission submission = new Submission
            {
                AchievedResult = RandomPoints(problem.Points),
                Code = model.Code,
                CreatedOn = DateTime.UtcNow,
                ProblemId = model.ProblemId,
                UserId = _userManager.GetUserId(User),
            };
            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();
            return Redirect("/");
        }

        private int RandomPoints(int maxValue)
        {
            Random r = new Random();
            int result = r.Next(0, maxValue);
            return result;
        }
    }
}