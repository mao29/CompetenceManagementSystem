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

namespace Application.CompetenceCategories.Queries.GetCompetenceCategories
{
    public class GetCompetenceCategoriesQuery : IRequest<CompetenceCategoriesListViewModel>
    {
    }

    public class GetCompetenceCategoriesQueryHandler : IRequestHandler<GetCompetenceCategoriesQuery, CompetenceCategoriesListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompetenceCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompetenceCategoriesListViewModel> Handle(GetCompetenceCategoriesQuery request, CancellationToken cancellationToken)
        {
            return new CompetenceCategoriesListViewModel()
            {
                CompetenceCategories = await _context.CompetenceCategories
                    .ProjectTo<CompetenceCategoryDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
