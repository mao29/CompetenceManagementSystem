using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Competences.Commands.CreateCompetence
{
    public class CreateCompetenceViewModel
    {
        public IEnumerable<CreateCompetenceCategoryDto> Caterogies { get; set; }
    }
}
