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

namespace Application.Resources.Commands.UpdateResource
{
    public class UpdateResourceCommand : IRequest, IMapFrom<Resource>
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Ссылка")]
        public string Link { get; set; }
    }

    public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateResourceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await _context.Resources.FindAsync(request.Id);

            resource.Name = request.Name;
            resource.Description = request.Description;
            resource.Link = request.Link;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(request.Id))
                {
                    throw new NotFoundException(nameof(Resource), request.Id);
                }
                else
                {
                    throw;
                }
            }

            return default;
        }

        private bool ResourceExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}
