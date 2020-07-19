using CompetenceManagementSystem.Domain.Common;
using CompetenceManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Domain.Entities
{
    /// <summary>
    /// Компетенция.
    /// </summary>
    public class Competence : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CompetenceType Type { get; set; }

        public CompetenceCategory Category { get; set; }

        public int CategoryId { get; set; }

        public ICollection<CompetenceResource> Resources { get; set; }

        public ICollection<EmployeeCompetence> Employees { get; set; }
    }
}
