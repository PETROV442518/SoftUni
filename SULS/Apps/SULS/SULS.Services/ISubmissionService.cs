using SULS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.Services
{
    public interface ISubmissionService
    {
        List<Submission> GetAllSubsForProblem(string Id);
        void Create(Submission submission);
        void Delete(string id);
    }
}

