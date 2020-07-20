using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Competences.Queries.GetCompetenceResourceDetails
{
    public class CompetenceResourceDetailsDto : IMapFrom<CompetenceResource>
    {
        public int CompetenceId { get; set; }

        [Display(Name = "Компетенция")]
        public string CompetenceName { get; set; }

        public int ResourceId { get; set; }

        [Display(Name = "Ресурс")]
        public string ResourceName { get; set; }

        [Display(Name = "Уровень владения")]
        public string Level { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        [Display(Name = "Дата создания")]
        public string Created { get; set; }

        [Display(Name = "Создатель")]
        public string CreatedBy { get; set; }

        [Display(Name = "Дата изменения")]
        public string LastModified { get; set; }

        [Display(Name = "Редактор")]
        public string LastModifiedBy { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CompetenceResource, CompetenceResourceDetailsDto>()
                .ForMember(x => x.CompetenceName, opt => opt.MapFrom(x => x.Competence.Name))
                .ForMember(x => x.ResourceName, opt => opt.MapFrom(x => x.Resource.Name))
                .ForMember(x => x.Level, opt => opt.MapFrom(x => x.Level.GetDisplayText()))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(d => d.LastModified, opt => opt.MapFrom(s => s.LastModified != null
                    ? s.LastModified.Value.ToString("dd.MM.yyyy hh:mm:ss")
                    : ""));
        }
    }
}
