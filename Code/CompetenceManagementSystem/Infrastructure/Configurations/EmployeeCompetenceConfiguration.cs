using CompetenceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Infrastructure.Configurations
{
    public class EmployeeCompetenceConfiguration : AuditableEntityConfiguration<EmployeeCompetence>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<EmployeeCompetence> builder)
        {
            builder.HasKey(x => new { x.CompetenceId, x.EmployeeId });

            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Competences)
                .HasForeignKey(x => x.EmployeeId);

            builder.HasOne(x => x.Competence)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompetenceId);
        }
    }
}
