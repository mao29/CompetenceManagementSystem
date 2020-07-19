using CompetenceManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Domain.Entities
{
    /// <summary>
    /// Сотрудник.
    /// </summary>
    public class Employee : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<EmployeeCompetence> Competences { get; set; }
    }
}
