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

namespace Application.Competences.Commands.CreateCompetenceResource
{
    public class CreateCompetenceResourceCommand : IRequest
    {
        public int CompetenceId { get; set; }

        [Display(Name = "Ресурс")]
        public int ResourceId { get; set; }

        [Display(Name = "Уровень владения")]
        public CompetenceLevel Level { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
    }

    public class CreateCompetenceResourceCommandHandler : IRequestHandler<CreateCompetenceResourceCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCompetenceResourceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCompetenceResourceCommand request, CancellationToken cancellationToken)
        {
            var entity = new CompetenceResource()
            {
                CompetenceId = request.CompetenceId,
                ResourceId = request.ResourceId,
                Comment = request.Comment,
                Level = request.Level
            };

            _context.CompetenceResources.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
