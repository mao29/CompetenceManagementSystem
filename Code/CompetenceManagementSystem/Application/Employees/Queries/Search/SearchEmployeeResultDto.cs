using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Employees.Queries.Search
{
    public class SearchEmployeeResultDto
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        public IList<SearchCompetenceResultDto> Competences { get; set; }

        public int Relevance { get; set; }

    }
}
