using CompetenceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Infrastructure.Configurations
{
    public class CompetenceCategoryConfiguration : AuditableEntityConfiguration<CompetenceCategory>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<CompetenceCategory> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
