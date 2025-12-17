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
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name).IsRequired().HasMaxLength(100);

            builder.Property(d => d.Description).IsRequired().HasMaxLength(250);

            builder.Property(d => d.CreatedBy).IsRequired().HasMaxLength(100);

            builder.Property(d => d.ModifiedBy).HasMaxLength(100);

            // RelationShips
            builder.HasMany(d => d.Employees).WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
