using CompetenceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Infrastructure.Configurations
{
    public class EmployeeConfiguration : AuditableEntityConfiguration<Employee>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(511);
        }
    }
}
