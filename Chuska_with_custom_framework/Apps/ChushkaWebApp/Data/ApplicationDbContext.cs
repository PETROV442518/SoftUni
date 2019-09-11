using ChushkaWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChushkaWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server =MYPC\SQLEXPRESS; Database = ChuskaDb; Trusted_Connection = True; Integrated Security = True; ");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(user => user.HasKey(u => u.Id));
            modelBuilder.Entity<Order>(order => order.HasKey(o => o.Id));
            modelBuilder.Entity<User>(product => product.HasKey(p => p.Id));
            base.OnModelCreating(modelBuilder);
        }
    }
}
