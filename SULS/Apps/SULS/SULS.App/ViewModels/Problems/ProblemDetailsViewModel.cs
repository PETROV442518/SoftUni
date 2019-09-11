using SULS.App.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Problems
{
    public class ProblemDetailsViewModel
    {
        public ProblemDetailsViewModel()
        {
            this.SubmissionsViewModel = new List<AllSubmissionsViewModel>();
        }

        public string Name { get; set; }
        public List<AllSubmissionsViewModel> SubmissionsViewModel { get; set; }
    }
}
