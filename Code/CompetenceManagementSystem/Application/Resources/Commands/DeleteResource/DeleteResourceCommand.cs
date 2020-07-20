using Application.Common.Exceptions;
using Application.Common.Interfaces;
using CompetenceManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Resources.Commands.DeleteResource
{
    public class DeleteResourceCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand>
    {

        private readonly IApplicationDbContext _context;

        public DeleteResourceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Resources.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Resource), request.Id);
            }

            _context.Resources.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
