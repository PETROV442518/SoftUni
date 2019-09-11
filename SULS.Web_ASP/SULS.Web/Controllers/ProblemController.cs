using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SULS.Data;
using SULS.Domain;
using SULS.Services;
using SULS.Web.Models.Problem;
using SULS.Web.Models.Submissions;

namespace SULS.Web.Controllers
{
    public class ProblemController : Controller
    {
        private readonly SULSDbContext _context;
        private readonly IProblemServices _problemServices;

        public ProblemController(SULSDbContext context, IProblemServices problemServices)
        {
            this._context = context;
            this._problemServices = problemServices;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProblemCreationBindingModel model)
        {
            Problem problem = new Problem
            {
                Name = model.Name,
                Points = model.Points,
            };
            this._context.Problems.Add(problem);
            await this._context.SaveChangesAsync();
            return Redirect("/");
        }

        public IActionResult Details(string id)
        {
            Problem problem = _context.Problems.FirstOrDefault(a => a.Id == id);
            List<Submission> submissions = _context.Submissions.Where(a => a.ProblemId == id).ToList();
            
            ProblemDetailsViewModel viewModel = new ProblemDetailsViewModel
            {
                Id = problem.Id,
                MaxPoints = problem.Points,
                Name = problem.Name,
                Submissions = submissions.Select(z => new SubmissionDetailsViewModel
                {
                    AchievedResult = z.AchievedResult,
                    CreatedOn = z.CreatedOn,
                    Id = z.Id,
                    Username = _context.Users.FirstOrDefault(x => x.Id == z.UserId).UserName
                }).ToList()
            };
            return View(viewModel);
        }
    }
}