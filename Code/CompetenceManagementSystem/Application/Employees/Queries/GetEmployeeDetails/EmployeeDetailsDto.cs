using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Application.Employees.Queries.GetEmployeeDetails
{
    public class EmployeeDetailsDto : IMapFrom<Employee>
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

        [Display(Name = "Компетенции")]
        public IList<EmployeeCompetenceDto> Competences { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(d => d.LastModified, opt => opt.MapFrom(s => s.LastModified != null
                    ? s.LastModified.Value.ToString("dd.MM.yyyy hh:mm:ss")
                    : ""))
                .ForMember(d => d.Competences, opt => opt.MapFrom(s => s.Competences
                    .OrderBy(x => x.Level)
                    .ThenBy(x => x.Competence.Name)));
        }
    }
}
