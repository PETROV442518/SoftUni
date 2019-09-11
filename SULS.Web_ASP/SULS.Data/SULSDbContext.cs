using SULS.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SULS.Data
{
    public class SULSDbContext : IdentityDbContext<IdentityUser>
    {
        public SULSDbContext(DbContextOptions<SULSDbContext> options) : base(options)
        {
        }

        public DbSet<Problem> Problems { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


    }
}
