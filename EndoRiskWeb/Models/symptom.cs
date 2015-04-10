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
        [StringLength(255, ErrorMessage = "Title can't be longer than 255 characters.")]
        public string symptom1 { get; set; }
        [Display(Name="Abbreviation")]
        [Required]
        [StringLength(5, ErrorMessage = "Title can't be longer than 5 characters.")]
        public string abbr { get; set; }
    }
}
