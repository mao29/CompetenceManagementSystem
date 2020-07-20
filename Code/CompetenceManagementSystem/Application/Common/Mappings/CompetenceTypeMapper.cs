using CompetenceManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Mappings
{
    public static class CompetenceTypeMapper
    {
        public static string GetDisplayText(this CompetenceType competenceType)
        {
            return competenceType switch
            {
                CompetenceType.Universal => "Универсальная",
                CompetenceType.Company => "Уровня компании",
                _ => throw new ArgumentException(nameof(competenceType))
            };
        }
    }
}
