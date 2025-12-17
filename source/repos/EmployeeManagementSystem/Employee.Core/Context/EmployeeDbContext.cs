using Employee.Dal.Configurations;
using Employee.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Context
{
    public class EmployeeDbContext : DbContext
    {

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> dbContextOptions) :base(dbContextOptions)
        {

        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfigurations());
            modelBuilder.ApplyConfiguration(new EmployeeConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            base.OnModelCreating(modelBuilder);
        }

    }
}
