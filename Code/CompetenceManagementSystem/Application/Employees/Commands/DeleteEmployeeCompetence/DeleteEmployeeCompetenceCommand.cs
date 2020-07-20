using Application.Common.Exceptions;
using Application.Common.Interfaces;
using CompetenceManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Commands.DeleteEmployeeCompetence
{
    public class DeleteEmployeeCompetenceCommand : IRequest
    {
        public int EmployeeId { get; set; }

        public int CompetenceId { get; set; }
    }

    public class DeleteEmployeeCompetenceCommandHandler : IRequestHandler<DeleteEmployeeCompetenceCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEmployeeCompetenceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteEmployeeCompetenceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.EmployeeCompetences.FindAsync(new object[] { request.CompetenceId, request.EmployeeId, }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(EmployeeCompetence), $"{request.EmployeeId} {request.CompetenceId}");
            }

            _context.EmployeeCompetences.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
