using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Competences.Commands.CreateCompetence
{
    public class CreateCompetenceCommandValidator : AbstractValidator<CreateCompetenceCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateCompetenceCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
             .MaximumLength(512)
             .NotEmpty()
             .Must(UniqueName)
             .WithMessage("Компетенция с таким наименованием уже существует в данной категории")
             .WithName("Наименование");
        }

        private bool UniqueName(CreateCompetenceCommand command, string name)
        {
            return _context.Competences.All(x => x.Name != name || x.CategoryId != command.CategoryId);
        }
    }
}
