using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Competences.Queries.GetCompetenceDetails
{
    public class CompetenceDetailsDto : IMapFrom<Competence>
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Категория")]
        public string CompetenceCategory { get; set; }

        [Display(Name = "Тип")]
        public string Type { get; set; }

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
            profile.CreateMap<Competence, CompetenceDetailsDto>()
                .ForMember(d => d.CompetenceCategory, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.GetDisplayText()))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(d => d.LastModified, opt => opt.MapFrom(s => s.LastModified != null 
                    ? s.LastModified.Value.ToString("dd.MM.yyyy hh:mm:ss") 
                    : ""));
        }
    }
}
