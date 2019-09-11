using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.Services
{
    public class ProblemService : IProblemService
    {
        private readonly SULSContext context;

        public ProblemService(SULSContext context)
        {
            this.context = context;
        }
        public void CreateProblem(Problem problem)
        {
            this.context.Problems.Add(problem);
            context.SaveChanges();
        }

        public IQueryable<Problem> GetAllProblems()
        {
            IQueryable<Problem> problemsFromDb = this.context.Problems;
            return problemsFromDb;
        }

        public Problem GetProblemById(string Id)
        {
            Problem problem = this.context.Problems.FirstOrDefault(a => a.Id == Id);
            return problem;
        }
    }
}
