using Application.Common.Interfaces;
using Application.Common.Mappings;
using CompetenceManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Queries.Search
{
    public class SearchEmployeesRequest : IRequest<SearchEmployeesResult>
    {
        public IList<SearchCompetenceRequest> Competences { get; set; }
    }

    public class SearchEmployeesRequestHandler : IRequestHandler<SearchEmployeesRequest, SearchEmployeesResult>
    {
        private readonly IApplicationDbContext _context;

        public SearchEmployeesRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SearchEmployeesResult> Handle(SearchEmployeesRequest request, CancellationToken cancellationToken)
        {
            var searchedCompetences = await _context.Competences
                .Where(x => request.Competences.Select(x => x.CompetenceId).Contains(x.Id))
                .ToListAsync();

            var competencesQuery = _context.EmployeeCompetences.Where(x => 1 != 1);
            foreach (var competence in request.Competences)
            {
                competencesQuery = competencesQuery
                    .Union(_context.EmployeeCompetences
                        .Where(x => x.CompetenceId == competence.CompetenceId
                            && (!competence.MinLevelRequired || x.Level >= competence.MinLevel)));
            }

            var groupedEmployees = (await competencesQuery.Select(x => new
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee.Name,
                CompetenceId = x.CompetenceId,
                Level = x.Level
            }).ToListAsync())
            .GroupBy(x => new { x.EmployeeId, x.EmployeeName });

            var resultEmployees = new List<SearchEmployeeResultDto>();
            foreach (var groupedEmployee in groupedEmployees)
            {
                bool isRelevant = true;
                var employee = new SearchEmployeeResultDto()
                {
                    Id = groupedEmployee.Key.EmployeeId,
                    Name = groupedEmployee.Key.EmployeeName
                };

                var competences = new List<SearchCompetenceResultDto>();
                foreach (var searchedCompetence in request.Competences)
                {
                    var level = groupedEmployee
                            .Where(x => x.CompetenceId == searchedCompetence.CompetenceId)
                            .FirstOrDefault()
                            ?.Level;

                    if (searchedCompetence.MinLevelRequired && !level.HasValue)
                    {
                        isRelevant = false;
                        break;
                    }

                    var employeeCompetence = new SearchCompetenceResultDto()
                    {
                        Name = searchedCompetences.First(x => x.Id == searchedCompetence.CompetenceId).Name,
                        Level = level?.GetDisplayText() ?? "-"
                    };
                    employeeCompetence.Distance = ComputeLevelDistance(searchedCompetence.MinLevel, level);

                    competences.Add(employeeCompetence);
                }

                if (!isRelevant)
                {
                    continue;
                }

                employee.Competences = competences;
                employee.Relevance = competences.Sum(x => x.Distance);

                resultEmployees.Add(employee);
            }

            return new SearchEmployeesResult()
            {
                Employees = resultEmployees.OrderByDescending(x => x.Relevance)
            };
        }

        private int ComputeLevelDistance(CompetenceLevel? requiredLevel, CompetenceLevel? actualLevel)
        {
            if (requiredLevel == null)
            {
                return 10;
            }

            if (actualLevel == null)
            {
                return 0;
            }

            return (actualLevel.Value - requiredLevel.Value) switch
            {
                2 => 8,
                1 => 6,
                0 => 10,
                -1 => 3,
                -2 => 1,
                _ => throw new NotImplementedException()
            };
        }
    }
}
