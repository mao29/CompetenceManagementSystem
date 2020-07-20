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

namespace Application.Competences.Queries.GetCompetences
{
    public class GetCompetencesQuery : IRequest<CompetencesListViewModel>
    {
    }

    public class GetCompetencesQueryHandler : IRequestHandler<GetCompetencesQuery, CompetencesListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompetencesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompetencesListViewModel> Handle(GetCompetencesQuery request, CancellationToken cancellationToken)
        {
            return new CompetencesListViewModel()
            {
                Competences = await _context.Competences
                    .ProjectTo<CompetenceDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
