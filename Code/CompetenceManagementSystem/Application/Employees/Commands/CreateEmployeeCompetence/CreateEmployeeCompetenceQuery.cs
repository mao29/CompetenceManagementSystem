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

namespace Application.Employees.Commands.CreateEmployeeCompetence
{
    public class CreateEmployeeCompetenceQuery : IRequest<CreateEmployeeCompetenceViewModel>
    {
        public int EmployeeId { get; set; }
    }

    public class CreateEmployeeCompetenceQueryHandler : IRequestHandler<CreateEmployeeCompetenceQuery, CreateEmployeeCompetenceViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateEmployeeCompetenceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateEmployeeCompetenceViewModel> Handle(CreateEmployeeCompetenceQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.Include(x => x.Competences).FirstOrDefaultAsync(x => x.Id == request.EmployeeId);
            var existingCompetencesIds = employee.Competences.Select(x => x.CompetenceId);

            return new CreateEmployeeCompetenceViewModel()
            {
                EmployeeName = employee.Name,
                EmployeeId = request.EmployeeId,
                Competences = await _context.Competences
                    .Where(x => !existingCompetencesIds.Contains(x.Id))
                    .ProjectTo<CreateEmployeeCompetenceDto>(_mapper.ConfigurationProvider)
                    .ToListAsync()
            };
        }
    }
}
