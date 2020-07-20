using Application.Common.Exceptions;
using Application.Common.Interfaces;
using CompetenceManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Competences.Commands.DeleteCompetence
{
    public class DeleteCompetenceCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCompetenceCommandHandler : IRequestHandler<DeleteCompetenceCommand>
    {

        private readonly IApplicationDbContext _context;

        public DeleteCompetenceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCompetenceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Competences.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Competence), request.Id);
            }

            _context.Competences.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
