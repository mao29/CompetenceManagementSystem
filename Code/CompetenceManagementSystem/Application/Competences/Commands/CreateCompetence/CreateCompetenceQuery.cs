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

namespace Application.Competences.Commands.CreateCompetence
{
    public class CreateCompetenceQuery : IRequest<CreateCompetenceViewModel>
    {
    }

    public class CreateCompetenceQueryHandler : IRequestHandler<CreateCompetenceQuery, CreateCompetenceViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCompetenceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateCompetenceViewModel> Handle(CreateCompetenceQuery request, CancellationToken cancellationToken)
        {
            return new CreateCompetenceViewModel()
            {
                Caterogies = await _context.CompetenceCategories
                    .ProjectTo<CreateCompetenceCategoryDto>(_mapper.ConfigurationProvider)
                    .ToListAsync()
            };
        }
    }
}
