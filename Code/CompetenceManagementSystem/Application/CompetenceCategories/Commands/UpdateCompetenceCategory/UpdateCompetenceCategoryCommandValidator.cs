using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.CompetenceCategories.Commands.UpdateCompetenceCategory
{
    public class UpdateCompetenceCategoryCommandValidator : AbstractValidator<UpdateCompetenceCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCompetenceCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
             .MaximumLength(255)
             .NotEmpty()
             .Must(UniqueName)
             .WithMessage("Категория с таким наименованием уже существует")
             .WithName("Наименование");
        }

        private bool UniqueName(UpdateCompetenceCategoryCommand command, string name)
        {
            return _context.CompetenceCategories.All(x => x.Name != name || x.Id == command.Id);
        }
    }
}
