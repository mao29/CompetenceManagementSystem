using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CompetenceCategories.Commands.UpdateCompetenceCategory
{
    public class UpdateCompetenceCategoryCommand : IRequest, IMapFrom<CompetenceCategory>
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }

    public class UpdateCompetenceCategoryCommandHandler : IRequestHandler<UpdateCompetenceCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCompetenceCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCompetenceCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.CompetenceCategories.FindAsync(request.Id);

            category.Name = request.Name;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetenceCategoryExists(request.Id))
                {
                    throw new NotFoundException(nameof(CompetenceCategory), request.Id);
                }
                else
                {
                    throw;
                }
            }

            return default;
        }

        private bool CompetenceCategoryExists(int id)
        {
            return _context.CompetenceCategories.Any(e => e.Id == id);
        }
    }
}
