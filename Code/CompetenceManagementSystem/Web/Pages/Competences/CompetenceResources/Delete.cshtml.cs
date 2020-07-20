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
using Application.Competences.Queries.GetCompetenceResourceDetails;
using Application.Competences.Commands.DeleteCompetenceResource;
using Application.Common.Exceptions;

namespace CompetenceManagementSystem.Web.Pages.Competences.CompetenceResources
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public CompetenceResourceDetailsDto Data { get; set; }

        public async Task<IActionResult> OnGetAsync(int resourceId, int competenceId)
        {
            Data = await _mediator.Send(new GetCompetenceResourceDetailsQuery() { ResourceId = resourceId, CompetenceId = competenceId });

            if (Data == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int resourceId, int competenceId)
        {
            try
            {
                await _mediator.Send(new DeleteCompetenceResourceCommand() { ResourceId = resourceId, CompetenceId = competenceId });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return RedirectToPage("../Details", new { id = competenceId });
        }
    }
}
