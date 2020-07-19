using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CompetenceManagementSystem.Domain.Entities;
using CompetenceManagementSystem.Infrastructure.Persistence;
using Application.Employees.Queries.GetEmployeeDetails;
using MediatR;

namespace CompetenceManagementSystem.Web.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public EmployeeDetailsDto Data { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Data = await _mediator.Send(new GetEmployeeDetailsQuery() { Id = id.Value });

            if (Data == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
