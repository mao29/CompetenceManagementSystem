using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Employees.Queries.Search
{
    public class SearchEmployeesRequestValidator : AbstractValidator<SearchEmployeesRequest>
    {
        public SearchEmployeesRequestValidator()
        {
            RuleFor(x => x.Competences)
                .Must(x => x != null && x.Count() > 0)
                .WithMessage("Укажите хотя бы одну компетенцию")
                .Must(x => x != null && x.Count() <= 10)
                .WithMessage("Укажите не более 10 компетенций");
        }
    }
}
