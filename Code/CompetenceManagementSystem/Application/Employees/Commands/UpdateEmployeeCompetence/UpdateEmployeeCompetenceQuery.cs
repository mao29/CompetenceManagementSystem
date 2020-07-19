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

namespace Application.Employees.Commands.UpdateEmployeeCompetence
{
    public class UpdateEmployeeCompetenceQuery : IRequest<UpdateEmployeeCompetenceCommand>
    {
        public int EmployeeId { get; set; }
        public int CompetenceId { get; set; }
    }

    public class UpdateEmployeeCompetenceQueryHandler : IRequestHandler<UpdateEmployeeCompetenceQuery, UpdateEmployeeCompetenceCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateEmployeeCompetenceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateEmployeeCompetenceCommand> Handle(UpdateEmployeeCompetenceQuery request, CancellationToken cancellationToken)
        {
            return await _context.EmployeeCompetences
               .ProjectTo<UpdateEmployeeCompetenceCommand>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId && x.CompetenceId == request.CompetenceId);
        }
    }
}
