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
using Application.Competences.Commands.UpdateCompetence;
using CompetenceManagementSystem.Domain.Enums;
using Application.Common.Mappings;
using FluentValidation;
using Application.Common.Exceptions;

namespace CompetenceManagementSystem.Web.Pages.Competences
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public UpdateCompetenceCommand Command { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Types => Enum.GetValues(typeof(CompetenceType))
            .Cast<CompetenceType>()
            .Select(x => new SelectListItem(x.GetDisplayText(), x.ToString()));

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await _mediator.Send(new UpdateCompetenceQuery() { Id = id.Value });

            Command = viewModel.Command;
            Categories = viewModel.Caterogies.Select(x => new SelectListItem(x.Name, x.Id.ToString()));

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
                return RedirectToPage("./Index");
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError($"{nameof(Command)}.{error.PropertyName}", error.ErrorMessage);
                }

                var viewModel = await _mediator.Send(new UpdateCompetenceQuery() { Id = Command.Id });
                Categories = viewModel.Caterogies.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
                return Page();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
