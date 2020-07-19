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

namespace Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeQuery : IRequest<UpdateEmployeeCommand>
    {
        public int Id { get; set; }
    }

    public class UpdateEmployeeQueryHandler : IRequestHandler<UpdateEmployeeQuery, UpdateEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateEmployeeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateEmployeeCommand> Handle(UpdateEmployeeQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .ProjectTo<UpdateEmployeeCommand>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
