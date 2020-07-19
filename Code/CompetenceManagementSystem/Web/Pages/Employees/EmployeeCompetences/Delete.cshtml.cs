using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompetenceManagementSystem.Domain.Entities;
using CompetenceManagementSystem.Infrastructure.Persistence;
using Application.Employees.Queries.GetEmployeeCompetenceDetails;
using MediatR;
using Application.Common.Exceptions;
using Application.Employees.Commands.DeleteEmployeeCompetence;

namespace CompetenceManagementSystem.Web.Pages.Employees.EmployeeCompetences
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int employeeId, int competenceId)
        {
            try
            {
                await _mediator.Send(new DeleteEmployeeCompetenceCommand() { EmployeeId = employeeId, CompetenceId = competenceId });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return RedirectToPage("../Details", employeeId);
        }
    }
}
