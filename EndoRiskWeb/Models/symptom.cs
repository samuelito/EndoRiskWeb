using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class symptom
    {
        public int idSymptom { get; set; }
        [Display(Name="Symptom")]
        [Required]
        public string symptom1 { get; set; }
        [Display(Name="Abbreviation")]
        [Required]
        public string abbr { get; set; }
    }
}
