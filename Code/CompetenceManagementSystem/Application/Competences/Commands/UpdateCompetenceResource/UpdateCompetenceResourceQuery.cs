using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Competences.Commands.UpdateCompetenceResource
{
    public class UpdateCompetenceResourceQuery : IRequest<UpdateCompetenceResourceCommand>
    {
        public int ResourceId { get; set; }
        public int CompetenceId { get; set; }
    }

    public class UpdateCompetenceResourceQueryHandler : IRequestHandler<UpdateCompetenceResourceQuery, UpdateCompetenceResourceCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCompetenceResourceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateCompetenceResourceCommand> Handle(UpdateCompetenceResourceQuery request, CancellationToken cancellationToken)
        {
            return await _context.CompetenceResources
               .ProjectTo<UpdateCompetenceResourceCommand>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(x => x.ResourceId == request.ResourceId && x.CompetenceId == request.CompetenceId);
        }
    }
}
