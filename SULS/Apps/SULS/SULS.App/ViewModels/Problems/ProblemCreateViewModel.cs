using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Web.ViewModels.Problems
{
    public class ProblemCreateViewModel
    {
        [RequiredSis]
        [StringLengthSis(5, 20, "Name must be between 5 and 20 symbols long.")]
        public string Name { get; set; }

        [RequiredSis]
        [RangeSis(50,300, "Points must be between 50 and 300.")]
        public int Points { get; set; }
    }
}
