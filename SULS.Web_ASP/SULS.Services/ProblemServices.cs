using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SULS.Data;
using SULS.Domain;
using SULS.Services.Dtos;

namespace SULS.Services
{
    public class ProblemServices : IProblemServices
    {
        private readonly SULSDbContext _context;

        public ProblemServices(SULSDbContext context)
        {
            this._context = context;
        }
        public List<ProblemDto> GetAllProblems()
        {
            List<ProblemDto> results = this._context.Problems.Select(a => new ProblemDto
            {
                Id = a.Id,
                Name = a.Name,
                Points = a.Points,
                Count = this._context.Submissions.Where( z => z.ProblemId == a.Id).Count()
            }).ToList();
            return results;
        }

        public ProblemDto GetProblemById(string id)
        {
            Problem problem = _context.Problems.FirstOrDefault(a => a.Id == id);
            ProblemDto dto = new ProblemDto
            {
                Id = problem.Id,
                Name = problem.Name,
                Points = problem.Points,
                Count = _context.Submissions.Where(a => a.ProblemId == problem.Id).Count(),
            };
            return dto;
        }
    }
}
