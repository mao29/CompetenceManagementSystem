using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Employees.Queries.GetEmployees
{
    public class EmployeeDto : IMapFrom<Employee>
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }
    }
}
