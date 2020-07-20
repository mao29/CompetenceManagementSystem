using CompetenceManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Employees.Queries.Search
{
    public class SearchCompetenceResultDto
    {
        public string Name { get; set; }

        public string Level { get; set; }

        public int Distance { get; set; }
    }
}
