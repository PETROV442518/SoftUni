using SULS.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SULS.Web.Models.Home
{
    public class AllProblemsViewModel
    {
        public AllProblemsViewModel()
        {
            this.Problems = new List<ProblemDto>();
        }
        public ICollection<ProblemDto> Problems { get; set; }
    }
}
