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

namespace Application.Competences.Commands.CreateCompetence
{
    public class CreateCompetenceCommand : IRequest<int>
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Тип")]
        public CompetenceType Type { get; set; }
    }

    public class CreateCompetenceCommandHandler : IRequestHandler<CreateCompetenceCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCompetenceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCompetenceCommand request, CancellationToken cancellationToken)
        {
            var entity = new Competence
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                Type = request.Type
            };

            _context.Competences.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
