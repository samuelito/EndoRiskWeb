using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class endoquestion
    {
        public long idQuestion { get; set; }
        [Display(Name="Question")]
        public string endoQuestion1 { get; set; }
        [Display(Name="Abbreviation")]
        public string abbr { get; set; }
        [Display(Name="Set of Answers")]
        public string choiceSet { get; set; }
    }
}
