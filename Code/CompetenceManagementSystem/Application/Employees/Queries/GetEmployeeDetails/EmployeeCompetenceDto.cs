using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using CompetenceManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Employees.Queries.GetEmployeeDetails
{
    public class EmployeeCompetenceDto : IMapFrom<EmployeeCompetence>
    {
        public int EmployeeId { get; set; }

        public int CompetenceId { get; set; }

        [Display(Name = "Компетенция")]
        public string Competence { get; set; }

        [Display(Name = "Уровень владения")]
        public CompetenceLevel Level { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeCompetence, EmployeeCompetenceDto>()
                .ForMember(x => x.Competence, opt => opt.MapFrom(x => x.Competence.Name));
        }
    }
}
