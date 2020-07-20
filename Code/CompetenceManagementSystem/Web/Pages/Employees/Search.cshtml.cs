using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Mappings;
using Application.Employees.Queries.Search;
using CompetenceManagementSystem.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompetenceManagementSystem.Web.Pages.Employees
{
    public class SearchModel : PageModel
    {
        private readonly IMediator _mediator;

        public SearchModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public SearchEmployeesRequest Request { get; set; }

        public IEnumerable<SelectListItem> Levels => Enum.GetValues(typeof(CompetenceLevel))
           .Cast<CompetenceLevel>()
           .Select(x => new SelectListItem(x.GetDisplayText(), x.ToString()));

        //  public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Competences { get; set; }

        public SearchEmployeesResult Result { get; set; }

        private async Task FillData()
        {
            if (Request.Competences == null)
            {
                Request.Competences = new List<SearchCompetenceRequest>();
            }
            var competences = await _mediator.Send(new GetSearchEmployeesQuery());
            //Categories = Competences
            //    .Select(x => x.Category)
            //    .Distinct()
            //    .Select(x => new SelectListItem(x, x));
            Competences = competences.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }

        public async Task OnGetAsync()
        {
            Request = new SearchEmployeesRequest()
            {
                Competences = new List<SearchCompetenceRequest>()
            };
            await FillData();
        }

        public async Task OnPostAddCompetenceRequest()
        {
            await FillData();
           
            Request.Competences.Add(new SearchCompetenceRequest());
        }

        public async Task OnPostRemoveCompetenceRequest(int index)
        {
            await FillData();

            Request.Competences.RemoveAt(index);
        }

        public async Task OnPostSearchAsync()
        {
            await FillData();
            
            try
            {
                Result = await _mediator.Send(Request);
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError($"{nameof(Request)}.{error.PropertyName}", error.ErrorMessage);
                }
            }
        }
    }
}