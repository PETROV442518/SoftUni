using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.App.ViewModels.Submissions;
using SULS.Models;
using SULS.Services;
using SULS.Web.ViewModels.Problems;
using System.Collections.Generic;

namespace SULS.App.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;
        private readonly ISubmissionService submissionService;
        private readonly IUserService userService;

        public ProblemsController(IProblemService problemService, ISubmissionService submissionService, IUserService userService)
        {
            this.problemService = problemService;
            this.submissionService = submissionService;
            this.userService = userService;
        }

       
      //
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            List<Submission> submissions = this.submissionService.GetAllSubsForProblem(id);
            ProblemDetailsViewModel viewModel = new ProblemDetailsViewModel();
            Problem problem = this.problemService.GetProblemById(id);


            foreach (var sub in submissions)
            {
                AllSubmissionsViewModel submission = new AllSubmissionsViewModel
                {
                    CreatedOn = sub.CreatedOn,
                    AchievedResult = sub.AchievedResult,
                    MaxPoints = problem.Points,
                    Username = this.userService.GetUserById(sub.UserId).Username,
                    SubmissionId = sub.Id,
                };
                viewModel.SubmissionsViewModel.Add(submission);
            }
            viewModel.Name = problem.Name;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ProblemCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Problems/Create");
            }
            Problem problemForDb = new Problem
            {
                Name = model.Name,
                Points = model.Points,
            };
            this.problemService.CreateProblem(problemForDb);
            return this.Redirect("/");
        }

       // [Authorize]
        //  public IActionResult Details()
        //  {
        //      return this.View();
        //  }
    }
}

