using Application.Common.Mappings;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Employees.Commands.CreateEmployeeCompetence
{
    public class CreateEmployeeCompetenceViewModel : IMapFrom<EmployeeCompetence>
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Сотрудник")]
        public string EmployeeName { get; set; }

        public IEnumerable<CreateEmployeeCompetenceDto> Competences { get; set; }
    }
}
