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

namespace Application.Employees.Queries.Search
{
    public class GetSearchEmployeesQuery : IRequest<IEnumerable<CompetenceDto>>
    {
    }

    public class GetSearchEmployeesQueryHandler : IRequestHandler<GetSearchEmployeesQuery, IEnumerable<CompetenceDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetSearchEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompetenceDto>> Handle(GetSearchEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Competences
                    .OrderBy(x => x.Category.Name)
                    .ThenBy(x => x.Name)
                    .ProjectTo<CompetenceDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }
    }
}
