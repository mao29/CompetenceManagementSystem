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

namespace Application.Competences.Queries.GetCompetenceResourceDetails
{
    public class GetCompetenceResourceDetailsQuery : IRequest<CompetenceResourceDetailsDto>
    {
        public int ResourceId { get; set; }
        public int CompetenceId { get; set; }
    }

    public class GetCompetenceResourceDetailsQueryHandler : IRequestHandler<GetCompetenceResourceDetailsQuery, CompetenceResourceDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompetenceResourceDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompetenceResourceDetailsDto> Handle(GetCompetenceResourceDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.CompetenceResources
                .ProjectTo<CompetenceResourceDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.ResourceId == request.ResourceId && x.CompetenceId == request.CompetenceId);
        }
    }
}
