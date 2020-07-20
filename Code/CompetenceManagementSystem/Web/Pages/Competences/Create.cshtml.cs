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
using Application.Competences.Commands.CreateCompetence;
using CompetenceManagementSystem.Domain.Enums;
using Application.Common.Mappings;
using FluentValidation;

namespace CompetenceManagementSystem.Web.Pages.Competences
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public CreateCompetenceCommand Command { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Types => Enum.GetValues(typeof(CompetenceType))
            .Cast<CompetenceType>()
            .Select(x => new SelectListItem(x.GetDisplayText(), x.ToString()));

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = (await _mediator.Send(new CreateCompetenceQuery())).Caterogies
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()));
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
                Categories = (await _mediator.Send(new CreateCompetenceQuery())).Caterogies
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString()));
                return Page();
            }
        }
    }
}
