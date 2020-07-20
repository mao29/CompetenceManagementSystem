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

namespace Application.Competences.Commands.UpdateCompetence
{
    public class UpdateCompetenceCommand : IRequest, IMapFrom<Competence>
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Тип")]
        public CompetenceType Type { get; set; }
    }

    public class UpdateCompetenceCommandHandler : IRequestHandler<UpdateCompetenceCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCompetenceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCompetenceCommand request, CancellationToken cancellationToken)
        {
            var competence = await _context.Competences.FindAsync(request.Id);

            if (competence == null)
            {
                throw new NotFoundException(nameof(Competence), request.Id);
            }

            competence.Name = request.Name;
            competence.CategoryId = request.CategoryId;
            competence.Type = request.Type;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetenceExists(request.Id))
                {
                    throw new NotFoundException(nameof(Competence), request.Id);
                }
                else
                {
                    throw;
                }
            }

            return default;
        }

        private bool CompetenceExists(int id)
        {
            return _context.Competences.Any(e => e.Id == id);
        }
    }
}
