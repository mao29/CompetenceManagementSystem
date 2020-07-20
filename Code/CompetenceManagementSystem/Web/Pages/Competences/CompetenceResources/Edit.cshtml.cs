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
using Application.Competences.Commands.UpdateCompetenceResource;
using CompetenceManagementSystem.Domain.Enums;
using Application.Common.Mappings;
using FluentValidation;
using Application.Common.Exceptions;

namespace CompetenceManagementSystem.Web.Pages.Competences.CompetenceResources
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public UpdateCompetenceResourceCommand Command { get; set; }

        public IEnumerable<SelectListItem> Levels => Enum.GetValues(typeof(CompetenceLevel))
            .Cast<CompetenceLevel>()
            .Select(x => new SelectListItem(x.GetDisplayText(), x.ToString()));

        public async Task<IActionResult> OnGetAsync(int resourceId, int competenceId)
        {
            Command = await _mediator.Send(new UpdateCompetenceResourceQuery() { ResourceId = resourceId, CompetenceId = competenceId });

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
                return RedirectToPage("../Details", new { id = Command.CompetenceId });
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
