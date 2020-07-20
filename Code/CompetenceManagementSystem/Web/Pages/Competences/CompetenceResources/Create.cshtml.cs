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
using Application.Competences.Commands.CreateCompetenceResource;
using CompetenceManagementSystem.Domain.Enums;
using Application.Common.Mappings;
using FluentValidation;

namespace CompetenceManagementSystem.Web.Pages.Competences.CompetenceResources
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public CreateCompetenceResourceViewModel Data { get; set; }

        [BindProperty]
        public CreateCompetenceResourceCommand Command { get; set; }

        public IEnumerable<SelectListItem> Levels => Enum.GetValues(typeof(CompetenceLevel))
            .Cast<CompetenceLevel>()
            .Select(x => new SelectListItem(x.GetDisplayText(), x.ToString()));

        public IEnumerable<SelectListItem> Resources { get; set; }

        public async Task<IActionResult> OnGetAsync(int competenceId)
        {
            Command = new CreateCompetenceResourceCommand() { CompetenceId = competenceId };
            Data = await _mediator.Send(new CreateCompetenceResourceQuery() { CompetenceId = competenceId });
            Resources = Data.Resources.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int competenceId)
        {
            try
            {
                await _mediator.Send(Command);
                return RedirectToPage("../Details", new { id = competenceId });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError($"{nameof(Command)}.{error.PropertyName}", error.ErrorMessage);
                }

                Data = await _mediator.Send(new CreateCompetenceResourceQuery() { CompetenceId = competenceId });
                Resources = Data.Resources.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
                return Page();
            }
        }
    }
}
