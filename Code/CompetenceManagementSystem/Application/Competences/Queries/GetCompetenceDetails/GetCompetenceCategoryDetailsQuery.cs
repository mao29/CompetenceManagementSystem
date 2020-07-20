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

namespace Application.Competences.Queries.GetCompetenceDetails
{
    public class GetCompetenceDetailsQuery : IRequest<CompetenceDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetCompetenceDetailsQueryHandler : IRequestHandler<GetCompetenceDetailsQuery, CompetenceDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompetenceDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CompetenceDetailsDto> Handle(GetCompetenceDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Competences
                .Include(x => x.Resources)
                .ThenInclude(x => x.Resource)
                .ProjectTo<CompetenceDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
