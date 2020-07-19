using Application.Common.Interfaces;
using CompetenceManagementSystem.Domain.Entities;
using CompetenceManagementSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Commands.CreateEmployeeCompetence
{
    public class CreateEmployeeCompetenceCommand : IRequest
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Компетенция")]
        public int CompetenceId { get; set; }

        [Display(Name = "Уровень владения")]
        public CompetenceLevel Level { get; set; }
    }

    public class CreateEmployeeCompetenceCommandHandler : IRequestHandler<CreateEmployeeCompetenceCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateEmployeeCompetenceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateEmployeeCompetenceCommand request, CancellationToken cancellationToken)
        {
            var entity = new EmployeeCompetence()
            {
                EmployeeId = request.EmployeeId,
                CompetenceId = request.CompetenceId,
                Level = request.Level
            };

            _context.EmployeeCompetences.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
