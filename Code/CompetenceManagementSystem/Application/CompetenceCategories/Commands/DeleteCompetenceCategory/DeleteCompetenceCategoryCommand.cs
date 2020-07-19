using Application.Common.Exceptions;
using Application.Common.Interfaces;
using CompetenceManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompetenceCategories.Commands.DeleteCompetenceCategory
{
    public class DeleteCompetenceCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCompetenceCategoryCommandHandler : IRequestHandler<DeleteCompetenceCategoryCommand>
    {

        private readonly IApplicationDbContext _context;

        public DeleteCompetenceCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCompetenceCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.CompetenceCategories.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CompetenceCategory), request.Id);
            }

            _context.CompetenceCategories.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
