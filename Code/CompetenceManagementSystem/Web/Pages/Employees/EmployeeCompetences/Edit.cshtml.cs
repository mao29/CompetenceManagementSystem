using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompetenceManagementSystem.Domain.Entities;
using CompetenceManagementSystem.Infrastructure.Persistence;
using MediatR;
using Application.Employees.Commands.UpdateEmployeeCompetence;
using FluentValidation;
using Application.Common.Exceptions;
using CompetenceManagementSystem.Domain.Enums;
using Application.Common.Mappings;

namespace CompetenceManagementSystem.Web.Pages.Employees.EmployeeCompetences
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public UpdateEmployeeCompetenceCommand Command { get; set; }

        public IEnumerable<SelectListItem> Levels => Enum.GetValues(typeof(CompetenceLevel))
            .Cast<CompetenceLevel>()
            .Select(x => new SelectListItem(x.GetDisplayText(), x.ToString()));

        public async Task<IActionResult> OnGetAsync(int employeeId, int competenceId)
        {
            Command = await _mediator.Send(new UpdateEmployeeCompetenceQuery() { EmployeeId = employeeId, CompetenceId = competenceId });

            if (Command == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _mediator.Send(Command);
                return RedirectToPage("../Details", Command.EmployeeId);
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError($"{nameof(Command)}.{error.PropertyName}", error.ErrorMessage);
                }
                return Page();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
