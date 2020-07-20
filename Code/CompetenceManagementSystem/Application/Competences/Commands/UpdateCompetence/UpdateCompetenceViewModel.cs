using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Competences.Commands.UpdateCompetence
{
    public class UpdateCompetenceViewModel
    {
        public UpdateCompetenceCommand Command { get; set; }

        public IEnumerable<UpdateCompetenceCategoryDto> Caterogies { get; set; }
    }
}
