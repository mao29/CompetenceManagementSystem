using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Competences.Commands.CreateCompetenceResource
{
    public class CreateCompetenceResourceQuery : IRequest<CreateCompetenceResourceViewModel>
    {
        public int CompetenceId { get; set; }
    }

    public class CreateCompetenceResourceQueryHandler : IRequestHandler<CreateCompetenceResourceQuery, CreateCompetenceResourceViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateCompetenceResourceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateCompetenceResourceViewModel> Handle(CreateCompetenceResourceQuery request, CancellationToken cancellationToken)
        {
            var competence = await _context.Competences.FirstOrDefaultAsync(x => x.Id == request.CompetenceId);

            return new CreateCompetenceResourceViewModel()
            {
                CompetenceId = request.CompetenceId,
                CompetenceName = competence.Name,
                Resources = await _context.Resources                    
                    .ProjectTo<CreateCompetenceResourceDto>(_mapper.ConfigurationProvider)
                    .ToListAsync()
            };
        }
    }
}
