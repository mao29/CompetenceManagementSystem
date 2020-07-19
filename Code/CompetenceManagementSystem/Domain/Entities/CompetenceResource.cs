using CompetenceManagementSystem.Domain.Common;
using CompetenceManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Domain.Entities
{
    /// <summary>
    /// Ресурс для овладения компетенцией.
    /// </summary>
    public class CompetenceResource : AuditableEntity
    {
        /// <summary>
        /// Уровень владения, которого позволяет достичь ресурс.
        /// </summary>
        public CompetenceLevel Level { get; set; }

        /// <summary>
        /// Комментарий к ресурсу.
        /// </summary>
        public string Comment { get; set; }

        public int CompetenceId { get; set; }

        public Competence Competence { get; set; }

        public int ResourceId { get; set; }

        public Resource Resource { get; set; }
    }
}
