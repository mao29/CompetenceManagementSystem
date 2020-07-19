using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateEmployeeCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)             
             .MaximumLength(511)
             .NotEmpty()
             .Must(UniqueName)
             .WithMessage("Сотрудник с таким именем уже существует")
             .WithName("Имя");
        }

        private bool UniqueName(string name)
        {
            return _context.Employees.All(x => x.Name != name);
        }
    }
}
