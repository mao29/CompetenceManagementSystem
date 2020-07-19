using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.CompetenceCategories.Queries.GetCompetenceCategoryDetails
{
    public class CompetenceCategoryDetailsDto : IMapFrom<CompetenceCategory>
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

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
            profile.CreateMap<CompetenceCategory, CompetenceCategoryDetailsDto>()
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(d => d.LastModified, opt => opt.MapFrom(s => s.LastModified != null 
                    ? s.LastModified.Value.ToString("dd.MM.yyyy hh:mm:ss") 
                    : ""));
        }
    }
}
