using CompetenceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Infrastructure.Configurations
{
    public class CompetenceResourceConfiguration : AuditableEntityConfiguration<CompetenceResource>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<CompetenceResource> builder)
        {
            builder.HasKey(x => new { x.CompetenceId, x.ResourceId });

            builder.HasOne(x => x.Competence)
                .WithMany(x => x.Resources)
                .HasForeignKey(x => x.CompetenceId);

            builder.HasOne(x => x.Resource)
                .WithMany(x => x.Competences)
                .HasForeignKey(x => x.ResourceId);
        }
    }
}
