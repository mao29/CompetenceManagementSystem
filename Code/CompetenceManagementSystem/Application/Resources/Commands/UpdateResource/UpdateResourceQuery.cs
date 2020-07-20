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

namespace Application.Resources.Commands.UpdateResource
{
    public class UpdateResourceQuery : IRequest<UpdateResourceCommand>
    {
        public int Id { get; set; }
    }

    public class UpdateResourceQueryHandler : IRequestHandler<UpdateResourceQuery, UpdateResourceCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateResourceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateResourceCommand> Handle(UpdateResourceQuery request, CancellationToken cancellationToken)
        {
            return await _context.Resources
                .ProjectTo<UpdateResourceCommand>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
