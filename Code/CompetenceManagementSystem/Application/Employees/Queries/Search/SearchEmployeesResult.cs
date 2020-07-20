using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Employees.Queries.Search
{
    public class SearchEmployeesResult
    {
        public IEnumerable<SearchEmployeeResultDto> Employees { get; set; }
    }
}
