using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Resources.Commands.UpdateResource
{
    public class UpdateResourceCommandValidator : AbstractValidator<UpdateResourceCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateResourceCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
             .MaximumLength(255)
             .NotEmpty()
             .Must(UniqueName)
             .WithMessage("Ресурс с таким наименованием уже существует")
             .WithName("Наименование");
        }

        private bool UniqueName(UpdateResourceCommand command, string name)
        {
            return _context.Resources.All(x => x.Name != name || x.Id == command.Id);
        }
    }
}
