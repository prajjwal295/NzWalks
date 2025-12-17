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
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(255);

            // Roles stored as JSON/string array (depends on provider)
            builder.Property(u => u.Roles)
                   .HasConversion(
                        roles => string.Join(",", roles),
                        roles => roles.Split(',', StringSplitOptions.RemoveEmptyEntries)
                   )
                   .HasColumnName("Roles");

            builder.Property(u => u.CreatedAt)
                   .IsRequired();

            builder.Property(u => u.LastLogin);
        }
    }
}
