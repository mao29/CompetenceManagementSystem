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

namespace CompetenceManagementSystem.Web.Pages.Competences.CompetenceResources
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public CompetenceResourceDetailsDto Data { get; set; }

        public async Task<IActionResult> OnGetAsync(int competenceId, int resourceId)
        {

            Data = await _mediator.Send(new GetCompetenceResourceDetailsQuery() { ResourceId = resourceId, CompetenceId = competenceId });

            if (Data == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
