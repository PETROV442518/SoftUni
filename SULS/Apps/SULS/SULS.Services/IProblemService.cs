using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.Services
{
    public interface IProblemService
    {
        void CreateProblem(Problem problem);
        IQueryable<Problem> GetAllProblems();
        Problem GetProblemById(string Id);
    }
}
