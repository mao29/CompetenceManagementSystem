using Application.Common.Mappings;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Competences.Commands.CreateCompetenceResource
{
    public class CreateCompetenceResourceViewModel : IMapFrom<CompetenceResource>
    {
        public int CompetenceId { get; set; }

        [Display(Name = "Компетенция")]
        public string CompetenceName { get; set; }

        public IEnumerable<CreateCompetenceResourceDto> Resources { get; set; }
    }
}
