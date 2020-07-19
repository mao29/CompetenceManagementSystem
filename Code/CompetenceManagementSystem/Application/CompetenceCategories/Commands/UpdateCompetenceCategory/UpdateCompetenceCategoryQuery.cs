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

namespace Application.CompetenceCategories.Commands.UpdateCompetenceCategory
{
    public class UpdateCompetenceCategoryQuery : IRequest<UpdateCompetenceCategoryCommand>
    {
        public int Id { get; set; }
    }

    public class UpdateCompetenceCategoryQueryHandler : IRequestHandler<UpdateCompetenceCategoryQuery, UpdateCompetenceCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateCompetenceCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateCompetenceCategoryCommand> Handle(UpdateCompetenceCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _context.CompetenceCategories
                .ProjectTo<UpdateCompetenceCategoryCommand>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
