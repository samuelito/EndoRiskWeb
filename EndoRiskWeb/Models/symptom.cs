using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class symptom
    {
        public int idSymptom { get; set; }
        [Display(Name="Sintoma")]
        [Required]
        [StringLength(255, ErrorMessage = "Sintoma no debe contener mas de 255 caracters.")]
        public string symptom1 { get; set; }
        [Display(Name="Abbreviatura")]
        [Required]
        [StringLength(10, ErrorMessage = "Abreviatura debe contener menos de 10 characters.")]
        public string abbr { get; set; }
    }
}
