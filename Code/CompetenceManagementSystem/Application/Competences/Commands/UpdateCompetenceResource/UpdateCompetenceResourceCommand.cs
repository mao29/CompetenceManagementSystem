using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using CompetenceManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Competences.Commands.UpdateCompetenceResource
{
    public class UpdateCompetenceResourceCommand : IRequest, IMapFrom<CompetenceResource>
    {
        public int CompetenceId { get; set; }
        public int ResourceId { get; set; }

        [Display(Name = "Компетенция")]
        public string CompetenceName { get; set; }

        [Display(Name = "Ресурс")]
        public string ResourceName { get; set; }

        [Display(Name = "Уровень владения")]
        public CompetenceLevel Level { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CompetenceResource, UpdateCompetenceResourceCommand>()
                .ForMember(x => x.CompetenceName, opt => opt.MapFrom(x => x.Competence.Name))
                .ForMember(x => x.ResourceName, opt => opt.MapFrom(x => x.Resource.Name));
        }
    }

    public class UpdateCompetenceResourceCommandHandler : IRequestHandler<UpdateCompetenceResourceCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCompetenceResourceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCompetenceResourceCommand request, CancellationToken cancellationToken)
        {
            var competenceResource = await _context.CompetenceResources.FindAsync(new object[] { request.CompetenceId, request.ResourceId }, cancellationToken);
            
            if (competenceResource == null)
            {
                throw new NotFoundException(nameof(CompetenceResource), $"{request.CompetenceId} {request.ResourceId}");
            }

            competenceResource.Comment = request.Comment;
            competenceResource.Level = request.Level;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetenceResourceExists(request.ResourceId, request.CompetenceId))
                {
                    throw new NotFoundException(nameof(CompetenceResource), $"{request.CompetenceId} {request.ResourceId}");
                }
                else
                {
                    throw;
                }
            }

            return default;
        }

        private bool CompetenceResourceExists(int resourceId, int competenceId)
        {
            return _context.CompetenceResources.Any(e => e.ResourceId == resourceId && e.CompetenceId == competenceId);
        }
    }
}
