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

namespace Application.CompetenceCategories.Queries.GetCompetenceCategoryDetails
{
    public class GetCompetenceCategoryDetailsQuery : IRequest<CompetenceCategoryDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetCompetenceCategoryDetailsQueryHandler : IRequestHandler<GetCompetenceCategoryDetailsQuery, CompetenceCategoryDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompetenceCategoryDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CompetenceCategoryDetailsDto> Handle(GetCompetenceCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.CompetenceCategories
                .ProjectTo<CompetenceCategoryDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
