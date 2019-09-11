using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly SULSContext context;

        public SubmissionService(SULSContext context)
        {
            this.context = context;
        }

        public void Create(Submission submission)
        {
            this.context.Submissions.Add(submission);
            this.context.SaveChanges();
        }

        public void Delete(string id)
        {
            Submission submission = this.context.Submissions.FirstOrDefault(a => a.Id == id);
            this.context.Submissions.Remove(submission);
            context.SaveChanges();

        }

        public List<Submission> GetAllSubsForProblem(string Id)
        {
            List<Submission> result = this.context.Submissions.Where(s => s.ProblemId == Id).ToList();
            return result;
        }
    }
}
