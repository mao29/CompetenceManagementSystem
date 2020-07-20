using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Competences.Commands.UpdateCompetence
{
    public class UpdateCompetenceCommandValidator : AbstractValidator<UpdateCompetenceCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCompetenceCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
             .MaximumLength(512)
             .NotEmpty()
             .Must(UniqueName)
             .WithMessage("Компетенция с таким наименованием уже существует в данной категории")
             .WithName("Наименование");
        }

        private bool UniqueName(UpdateCompetenceCommand command, string name)
        {
            return _context.Competences.All(x => x.Name != name || x.CategoryId != command.CategoryId || x.Id == command.Id);
        }
    }
}
