using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompetenceManagementSystem.Domain.Entities;
using CompetenceManagementSystem.Infrastructure.Persistence;
using Application.CompetenceCategories.Queries.GetCompetenceCategoryDetails;
using MediatR;
using Application.CompetenceCategories.Commands.DeleteCompetenceCategory;
using Application.Common.Exceptions;

namespace CompetenceManagementSystem.Web.Pages.CompetenceCategories
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public CompetenceCategoryDetailsDto Data { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Data = await _mediator.Send(new GetCompetenceCategoryDetailsQuery() { Id = id.Value });

            if (Data == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _mediator.Send(new DeleteCompetenceCategoryCommand() { Id = id.Value });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
