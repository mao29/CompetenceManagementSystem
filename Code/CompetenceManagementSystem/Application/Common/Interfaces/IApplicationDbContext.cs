using CompetenceManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Competence> Competences { get; set; }
        public DbSet<CompetenceCategory> CompetenceCategories { get; set; }

        public DbSet<CompetenceResource> CompetenceResources { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeCompetence> EmployeeCompetences { get; set; }

        public DbSet<Resource> Resources { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
