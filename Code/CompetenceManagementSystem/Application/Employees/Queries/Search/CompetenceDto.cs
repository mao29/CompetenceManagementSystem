using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Employees.Queries.Search
{
    public class CompetenceDto : IMapFrom<Competence>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Competence, CompetenceDto>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));
        }
    }
}
