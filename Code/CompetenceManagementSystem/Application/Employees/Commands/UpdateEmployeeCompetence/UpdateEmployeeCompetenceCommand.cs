using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using CompetenceManagementSystem.Domain.Entities;
using CompetenceManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Employees.Commands.UpdateEmployeeCompetence
{
    public class UpdateEmployeeCompetenceCommand : IRequest, IMapFrom<EmployeeCompetence>
    {
        public int EmployeeId { get; set; }
        public int CompetenceId { get; set; }

        [Display(Name = "Сотрудник")]
        public string EmployeeName { get; set; }

        [Display(Name = "Компетенция")]
        public string CompetenceName { get; set; }

        [Display(Name = "Уровень владения")]
        public CompetenceLevel Level { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EmployeeCompetence, UpdateEmployeeCompetenceCommand>()
                .ForMember(x => x.CompetenceName, opt => opt.MapFrom(x => x.Competence.Name))
                .ForMember(x => x.EmployeeName, opt => opt.MapFrom(x => x.Employee.Name));
        }
    }

    public class UpdateEmployeeCompetenceCommandHandler : IRequestHandler<UpdateEmployeeCompetenceCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEmployeeCompetenceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEmployeeCompetenceCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.EmployeeCompetences.FindAsync(new[] { request.EmployeeId, request.CompetenceId }, cancellationToken);
            
            if (employee == null)
            {
                throw new NotFoundException(nameof(EmployeeCompetence), $"{request.EmployeeId} {request.CompetenceId}");
            }
            
            employee.Level = request.Level;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeCompetenceExists(request.EmployeeId, request.CompetenceId))
                {
                    throw new NotFoundException(nameof(EmployeeCompetence), $"{request.EmployeeId} {request.CompetenceId}");
                }
                else
                {
                    throw;
                }
            }

            return default;
        }

        private bool EmployeeCompetenceExists(int employeeId, int competenceId)
        {
            return _context.EmployeeCompetences.Any(e => e.EmployeeId == employeeId && e.CompetenceId == competenceId);
        }
    }
}
