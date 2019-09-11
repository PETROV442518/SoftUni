using SULS.Web.ViewModels.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.App.ViewModels.Home
{
    public class AllProblemsHomeViewModel
    {
        public AllProblemsHomeViewModel()
        {
            this.Problems = new List<ProblemHomeViewModel>();
        }
        public List<ProblemHomeViewModel> Problems { get; set; }
    }
}
