using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CompetenceManagementSystem.Domain.Entities;
using CompetenceManagementSystem.Infrastructure.Persistence;
using MediatR;
using Application.Employees.Commands.CreateEmployeeCompetence;
using FluentValidation;
using CompetenceManagementSystem.Domain.Enums;
using Application.Common.Mappings;

namespace CompetenceManagementSystem.Web.Pages.Employees.EmployeeCompetences
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public CreateEmployeeCompetenceViewModel Data { get; set; }

        public IEnumerable<SelectListItem> Levels => Enum.GetValues(typeof(CompetenceLevel))
            .Cast<CompetenceLevel>()
            .Select(x => new SelectListItem(x.GetDisplayText(), x.ToString()));

        public IEnumerable<SelectListItem> Competences { get; set; }

        public async Task<IActionResult> OnGetAsync(int employeeId)
        {
            Data = await _mediator.Send(new CreateEmployeeCompetenceQuery() { EmployeeId = employeeId });
            Competences = Data.Competences.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _mediator.Send(Data.CreateCommand);
                return RedirectToPage("../Details", Data.CreateCommand.EmployeeId);
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError($"{nameof(Data.CreateCommand)}.{error.PropertyName}", error.ErrorMessage);
                }
                return Page();
            }
        }
    }
}
