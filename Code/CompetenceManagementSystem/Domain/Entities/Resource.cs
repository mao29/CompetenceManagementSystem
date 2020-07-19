using CompetenceManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompetenceManagementSystem.Domain.Entities
{
    /// <summary>
    /// Ресурс для изучения.
    /// </summary>
    public class Resource : AuditableEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Наменование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ссылка на ресурс.
        /// </summary>
        public string Link { get; set; }

        public ICollection<CompetenceResource> Competences { get; set; }
    }
}
