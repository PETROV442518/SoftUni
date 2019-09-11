using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SULS.Web.Models.Submissions
{
    public class SubmissionCreateBindingModel
    {
        public string ProblemId { get; set; }

        [MinLength(30)]
        [MaxLength(800)]
        [Required]
        public string Code { get; set; }
   
    }
}
