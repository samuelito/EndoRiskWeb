using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class endoquestion
    {
        public long idQuestion { get; set; }
        [Display(Name="Question")]
        [StringLength(255, ErrorMessage = "Question can't be longer than 255 characters.")]
        public string endoQuestion1 { get; set; }
        [Display(Name="Abbreviation")]
        [StringLength(5, ErrorMessage = "Abbreviation can't be longer than 5 characters.")]
        public string abbr { get; set; }
        [Display(Name="Set of Answers")]
        [StringLength(5, ErrorMessage = "Set can't be longer than 5 characters.")]
        public string choiceSet { get; set; }
    }
}
