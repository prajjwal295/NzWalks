using Employee.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Dal.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            {
                builder.ToTable("Employees");
                builder.HasKey(e => e.Id);
                builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
                builder.Property(e => e.ModifiedBy).HasMaxLength(100);
                builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
                builder.Property(e => e.PhoneNumber)
                    .HasMaxLength(15);
                builder.HasAlternateKey(e => e.Email);

                builder.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);
            }
        }
    }
}
