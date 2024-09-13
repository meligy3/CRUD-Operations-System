using Company.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Contexts
{
    public class CompanyDbContext:IdentityDbContext<ApplicationUser>
    { // identity user , identity roles
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options):base(options)
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18,2)");
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)   
        //  =>  optionsBuilder.UseSqlServer("Server=.;Database=CompanyMVCG02;Trusted_Connection=true");

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set;  }
        
       
    }
}
