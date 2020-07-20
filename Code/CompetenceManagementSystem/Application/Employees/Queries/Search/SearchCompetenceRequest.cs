using CompetenceManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Employees.Queries.Search
{
    public class SearchCompetenceRequest
    {
        public string Category { get; set; }

        public int CompetenceId { get; set; }
        
        public CompetenceLevel? MinLevel { get; set; } 

        public bool MinLevelRequired { get; set; }
    }
}
