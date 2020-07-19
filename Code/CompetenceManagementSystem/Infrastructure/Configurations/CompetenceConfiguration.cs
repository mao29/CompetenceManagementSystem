using CompetenceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Infrastructure.Configurations
{
    public class CompetenceConfiguration : AuditableEntityConfiguration<Competence>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Competence> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(512);
        }
    }
}
