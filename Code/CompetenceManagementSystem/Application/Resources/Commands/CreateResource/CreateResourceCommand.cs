using Application.Common.Interfaces;
using CompetenceManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Resources.Commands.CreateResource
{
    public class CreateResourceCommand : IRequest<int>
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Ссылка")]
        public string Link { get; set; }
    }

    public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateResourceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateResourceCommand request, CancellationToken cancellationToken)
        {
            var entity = new Resource
            {
                Name = request.Name,
                Description = request.Description,
                Link = request.Link
            };

            _context.Resources.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
