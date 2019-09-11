using SULS.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Services
{
    public interface IProblemServices
    {
        List<ProblemDto> GetAllProblems();
        ProblemDto GetProblemById(string id);
    }
}
