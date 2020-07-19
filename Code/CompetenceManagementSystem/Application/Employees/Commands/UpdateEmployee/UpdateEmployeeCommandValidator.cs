using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateEmployeeCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
             .MaximumLength(511)
             .NotEmpty()
             .Must(UniqueName)
             .WithMessage("Сотрудник с таким именем уже существует")
             .WithName("Имя");
        }

        private bool UniqueName(UpdateEmployeeCommand command, string name)
        {
            return _context.Employees.All(x => x.Name != name || x.Id == command.Id);
        }
    }
}
