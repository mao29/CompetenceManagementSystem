using Application.Common.Exceptions;
using Application.Common.Interfaces;
using CompetenceManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Competences.Commands.DeleteCompetenceResource
{
    public class DeleteCompetenceResourceCommand : IRequest
    {
        public int ResourceId { get; set; }

        public int CompetenceId { get; set; }
    }

    public class DeleteCompetenceResourceCommandHandler : IRequestHandler<DeleteCompetenceResourceCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCompetenceResourceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCompetenceResourceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.CompetenceResources.FindAsync(new object[] { request.CompetenceId, request.ResourceId, }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CompetenceResource), $"{request.CompetenceId} {request.ResourceId}");
            }

            _context.CompetenceResources.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
