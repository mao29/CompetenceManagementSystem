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
using Application.Resources.Queries.GetResources;

namespace CompetenceManagementSystem.Web.Pages.Resources
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ResourceListViewModel Data { get; private set; }

        public async Task OnGetAsync()
        {
            Data = await _mediator.Send(new GetResourcesQuery());
        }
    }
}
