using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SULS.Web.Models.Submissions
{
    public class SubmissionDetailsViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public int AchievedResult { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
