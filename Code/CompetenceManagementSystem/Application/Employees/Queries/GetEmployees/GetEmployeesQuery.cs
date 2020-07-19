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

namespace Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery : IRequest<EmployeesListViewModel>
    {
    }

    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, EmployeesListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeesListViewModel> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return new EmployeesListViewModel()
            {
                Employees = await _context.Employees
                    .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
