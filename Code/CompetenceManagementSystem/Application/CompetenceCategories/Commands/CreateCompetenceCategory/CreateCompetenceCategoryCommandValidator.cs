using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.CompetenceCategories.Commands.CreateCompetenceCategory
{
    public class CreateCompetenceCategoryCommandValidator : AbstractValidator<CreateCompetenceCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateCompetenceCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)             
             .MaximumLength(255)
             .NotEmpty()
             .Must(UniqueName)
             .WithMessage("Категория с таким наименованием уже существует")
             .WithName("Наименование");
        }

        private bool UniqueName(string name)
        {
            return _context.CompetenceCategories.All(x => x.Name != name);
        }
    }
}
