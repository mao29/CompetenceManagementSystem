using Application.Common.Mappings;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Competences.Commands.UpdateCompetence
{
    public class UpdateCompetenceCategoryDto : IMapFrom<CompetenceCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
