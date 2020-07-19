using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.CompetenceCategories.Queries.GetCompetenceCategories
{
    public class CompetenceCategoryDto : IMapFrom<CompetenceCategory>
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}
