using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Home;
using SULS.Models;
using SULS.Services;
using SULS.Web.ViewModels.Problems;
using System.Collections.Generic;
using System.Linq;

namespace SULS.App.Controllers
{
    public class HomeController :Controller

    {
       private readonly ISubmissionService submissionService;

        public IProblemService ProblemService { get; }

        public HomeController(IProblemService problemService, ISubmissionService submissionService)
        {
            ProblemService = problemService;
            this.submissionService = submissionService;
        }

        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        [Authorize]
        public IActionResult IndexLoggedIn()
        {
            AllProblemsHomeViewModel viewModelResult = new AllProblemsHomeViewModel();
            IQueryable<Problem> problems = this.ProblemService.GetAllProblems();
            foreach (var problem in problems.ToList())
            {
                viewModelResult.Problems.Add( new ProblemHomeViewModel
                {
                    Name = problem.Name,
                    Count = this.submissionService.GetAllSubsForProblem(problem.Id).Count,
                    Id = problem.Id,
                });
            }
            return this.View(viewModelResult);
        }
        public IActionResult Index()
        {
            if(IsLoggedIn())
            {
                return this.IndexLoggedIn();
            }
            return this.View();
        }
    }
}