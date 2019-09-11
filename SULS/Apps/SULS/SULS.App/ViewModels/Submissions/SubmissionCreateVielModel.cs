using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Submissions
{
    public class SubmissionCreateVielModel
    {
        [RequiredSis]
        [StringLengthSis(30, 800, "Code must be between 30 and 800 symbols long.")]
        public string Code { get; set; }
       
        public string ProblemId { get; set; }
       
    }
}
