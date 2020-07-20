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

namespace Application.Resources.Queries.GetResources
{
    public class GetResourcesQuery : IRequest<ResourceListViewModel>
    {
    }

    public class GetResourcesQueryHandler : IRequestHandler<GetResourcesQuery, ResourceListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetResourcesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResourceListViewModel> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
        {
            return new ResourceListViewModel()
            {
                Resources = await _context.Resources
                    .ProjectTo<ResourceDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
