using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompetenceManagementSystem.Domain.Entities;
using CompetenceManagementSystem.Infrastructure.Persistence;
using MediatR;
using Application.Employees.Queries.GetEmployeeCompetenceDetails;

namespace CompetenceManagementSystem.Web.Pages.Employees.EmployeeCompetences
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public EmployeeCompetenceDetailsDto Data { get; set; }

        public async Task<IActionResult> OnGetAsync(int employeeId, int competenceId)
        {

            Data = await _mediator.Send(new GetEmployeeCompetenceDetailsQuery() { EmployeeId = employeeId, CompetenceId = competenceId });

            if (Data == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
