using CompetenceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Infrastructure.Configurations
{
    public class ResourceConfiguration : AuditableEntityConfiguration<Resource>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Resource> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(511);          
        }
    }
}
