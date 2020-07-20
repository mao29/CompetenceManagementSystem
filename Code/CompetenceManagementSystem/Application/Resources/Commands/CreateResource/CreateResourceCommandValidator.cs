using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Resources.Commands.CreateResource
{
    public class CreateResourceCommandValidator : AbstractValidator<CreateResourceCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateResourceCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)             
             .MaximumLength(511)
             .NotEmpty()
             .Must(UniqueName)
             .WithMessage("Ресурс с таким наименованием уже существует")
             .WithName("Наименование");
        }

        private bool UniqueName(string name)
        {
            return _context.Resources.All(x => x.Name != name);
        }
    }
}
