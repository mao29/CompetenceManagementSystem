using Application.Common.Mappings;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Competences.Commands.CreateCompetenceResource
{
    public class CreateCompetenceResourceDto : IMapFrom<Resource>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
