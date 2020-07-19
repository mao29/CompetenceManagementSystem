using Application.Common.Interfaces;
using CompetenceManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompetenceCategories.Commands.CreateCompetenceCategory
{
    public class CreateCompetenceCategoryCommand : IRequest<int>
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }

    public class CreateCompetenceCategoryCommandHandler : IRequestHandler<CreateCompetenceCategoryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCompetenceCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCompetenceCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new CompetenceCategory
            {
                Name = request.Name
            };

            _context.CompetenceCategories.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
