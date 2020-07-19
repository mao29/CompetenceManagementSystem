using Application.Common.Mappings;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Employees.Commands.CreateEmployeeCompetence
{
    public class CreateEmployeeCompetenceDto : IMapFrom<Competence>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
