using CompetenceManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Mappings
{
    public static class CompetenceLevelMapper
    {
        public static string GetDisplayText(this CompetenceLevel competenceLevel)
        {
            return competenceLevel switch
            {
                CompetenceLevel.Basic => "Начальный",
                CompetenceLevel.Intermediate => "Средний",
                CompetenceLevel.Advanced => "Продвинутый",
                _ => throw new ArgumentOutOfRangeException(nameof(competenceLevel)),
            };
        }
    }
}
