using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.App.ViewModels.Submissions;
using SULS.Models;
using SULS.Services;
using System;

namespace SULS.App.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly IProblemService problemService;
        private readonly ISubmissionService submissionService;
        private readonly IUserService userService;

        public SubmissionsController(IProblemService problemService, ISubmissionService submissionService, IUserService userService)
        {
            this.problemService = problemService;
            this.submissionService = submissionService;
            this.userService = userService;
        }
        [Authorize]
        public IActionResult Create(string id)
        {
            Problem problem = this.problemService.GetProblemById(id);
            ProblemSubmitViewModel problemModel = new ProblemSubmitViewModel
            {
                Name = problem.Name,
                ProblemId = problem.Id,
            ProblemTotalPoints = problem.Points,
            };
            return this.View(problemModel);
        }

         [HttpPost]
         [Authorize]
         public IActionResult Create(SubmissionCreateVielModel model)
         {
            if(!ModelState.IsValid)
            {
                return this.Redirect("/Submissions/Create");
            }
            Problem problem = problemService.GetProblemById(model.ProblemId);
            Submission submission = new Submission
            {
                Code = model.Code,
                ProblemId = model.ProblemId,
                CreatedOn = DateTime.UtcNow,
                UserId = this.User.Id,
                AchievedResult = GenerateRandomNumber(problem.Points),
                
            };
            this.submissionService.Create(submission);
            return this.Redirect("/");
         }
        
        [Authorize]
        public IActionResult Delete(string id)
        {
            this.submissionService.Delete(id);
            return this.Redirect("/");
        }
        private int GenerateRandomNumber (int input)
        {
            Random r = new Random();
            int result = r.Next(50, input);
            return result;
        }

        
    }
}
