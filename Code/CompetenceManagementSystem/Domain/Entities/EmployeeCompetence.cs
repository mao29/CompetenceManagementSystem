using CompetenceManagementSystem.Domain.Common;
using CompetenceManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Domain.Entities
{
    /// <summary>
    /// Уровень владения компетенцией сотрудника.
    /// </summary>
    public class EmployeeCompetence : AuditableEntity
    {
        /// <summary>
        /// Уровень владения.
        /// </summary>
        public CompetenceLevel Level { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int CompetenceId { get; set; }

        public Competence Competence { get; set; }
    }
}
