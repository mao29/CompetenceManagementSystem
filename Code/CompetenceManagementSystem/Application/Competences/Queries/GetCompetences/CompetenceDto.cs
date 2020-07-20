using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Competences.Queries.GetCompetences
{
    public class CompetenceDto : IMapFrom<Competence>
    {
        public int Id { get; set; }

        [Display(Name = "Категория")]
        public string CompetenceCategory { get; set; }

        [Display(Name = "Тип")]
        public string Type { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Competence, CompetenceDto>()
                .ForMember(d => d.CompetenceCategory, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.GetDisplayText()));
        }
    }
}
