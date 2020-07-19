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

namespace Application.Employees.Queries.GetEmployeeCompetenceDetails
{
    public class GetEmployeeCompetenceDetailsQuery : IRequest<EmployeeCompetenceDetailsDto>
    {
        public int EmployeeId { get; set; }
        public int CompetenceId { get; set; }
    }

    public class GetEmployeeCompetenceDetailsQueryHandler : IRequestHandler<GetEmployeeCompetenceDetailsQuery, EmployeeCompetenceDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeCompetenceDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeCompetenceDetailsDto> Handle(GetEmployeeCompetenceDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.EmployeeCompetences
                .ProjectTo<EmployeeCompetenceDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId && x.CompetenceId == request.CompetenceId);
        }
    }
}
