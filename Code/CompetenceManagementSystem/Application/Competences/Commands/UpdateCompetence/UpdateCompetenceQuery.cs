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

namespace Application.Competences.Commands.UpdateCompetence
{
    public class UpdateCompetenceQuery : IRequest<UpdateCompetenceViewModel>
    {
        public int Id { get; set; }
    }

    public class UpdateCompetenceQueryHandler : IRequestHandler<UpdateCompetenceQuery, UpdateCompetenceViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateCompetenceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateCompetenceViewModel> Handle(UpdateCompetenceQuery request, CancellationToken cancellationToken)
        {
            return new UpdateCompetenceViewModel()
            {
                Command = await _context.Competences
                    .ProjectTo<UpdateCompetenceCommand>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id),
                Caterogies = await _context.CompetenceCategories
                    .ProjectTo<UpdateCompetenceCategoryDto>(_mapper.ConfigurationProvider)
                    .ToListAsync()
            };
        }
    }
}
