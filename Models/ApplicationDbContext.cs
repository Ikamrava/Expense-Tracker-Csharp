using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Expense_Traker_Csharp.Models
{
    public class ApplicationDbContext : DbContext
    {



        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         // Replace "YourConnectionString" with the actual connection string for your PostgreSQL database
        //         optionsBuilder.UseNpgsql("connectionString");
        //     }
        // }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DotNetEnv.Env.Load();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL"));
            }
            // optionsBuilder.UseNpgsql("DevConnection");
        }
        public DbSet<Transaction> transaction { get; set; }
        public DbSet<Category> category { get; set; }

    }
}