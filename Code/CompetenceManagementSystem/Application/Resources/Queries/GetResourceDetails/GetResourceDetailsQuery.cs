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

namespace Application.Resources.Queries.GetResourceDetails
{
    public class GetResourceDetailsQuery : IRequest<ResourceDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetResourceDetailsQueryHandler : IRequestHandler<GetResourceDetailsQuery, ResourceDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetResourceDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResourceDetailsDto> Handle(GetResourceDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Resources
                .ProjectTo<ResourceDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
