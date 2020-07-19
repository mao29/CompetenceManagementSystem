using CompetenceManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Domain.Entities
{
    /// <summary>
    /// Категория компетенции.
    /// </summary>
    public class CompetenceCategory : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Competence> Competences { get; set; }
    }
}
