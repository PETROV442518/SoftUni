using SULS.Web.Models.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SULS.Web.Models.Problem
{
    public class ProblemDetailsViewModel
    {
        public ProblemDetailsViewModel()
        {
            this.Submissions = new List<SubmissionDetailsViewModel>();
        }
        public string Name { get; set; }
        public string Id { get; set; }
        public int MaxPoints { get; set; }
        public ICollection<SubmissionDetailsViewModel> Submissions { get; set; }
    }
}
