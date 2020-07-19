using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Employees.Commands.CreateEmployeeCompetence
{
    public class CreateEmployeeCompetenceCommandValidator : AbstractValidator<CreateEmployeeCompetenceCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateEmployeeCompetenceCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.CompetenceId)
                .Must(Unique)
                .WithMessage("Данная компетенция уже есть у сотрудника")
                .NotNull()
                .WithName("Компетенция");
        }

        private bool Unique(CreateEmployeeCompetenceCommand command, int competenceId)
        {
            return _context.EmployeeCompetences.All(x => x.CompetenceId != competenceId || x.EmployeeId != command.EmployeeId);
        }
    }
}
