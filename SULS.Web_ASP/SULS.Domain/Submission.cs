using Microsoft.AspNetCore.Identity;
using System;

namespace SULS.Domain
{
    public class Submission
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public int AchievedResult { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string ProblemId { get; set; }
        public Problem Problem { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
