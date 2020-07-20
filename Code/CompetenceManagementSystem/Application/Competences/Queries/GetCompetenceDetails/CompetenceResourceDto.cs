using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Competences.Queries.GetCompetenceDetails
{
    public class CompetenceResourceDto : IMapFrom<CompetenceResource>
    {
        public int CompetenceId { get; set; }
        public int ResourceId { get; set; }

        [Display(Name = "Ресурс")]
        public string Resource { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        [Display(Name = "Уровень владения")]
        public string Level { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CompetenceResource, CompetenceResourceDto>()
                .ForMember(x => x.Resource, opt => opt.MapFrom(x => x.Resource.Name))
                .ForMember(x => x.Level, opt => opt.MapFrom(x => x.Level.GetDisplayText()));
        }
    }
}
