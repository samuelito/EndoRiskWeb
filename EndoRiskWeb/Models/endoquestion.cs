using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class endoquestion
    {
        public long idQuestion { get; set; }
        [Display(Name="Pregunta")]
        [StringLength(255, ErrorMessage = "La pregunta no debe tener mas de 255 caracteres.")]
        public string endoQuestion1 { get; set; }
        [Display(Name="Abbreviatura")]
        [StringLength(5, ErrorMessage = "La abbreviatura debe tener menos de 5 caracteres.")]
        public string abbr { get; set; }
        [Display(Name="Set de respuestas")]
        [StringLength(5, ErrorMessage = "El set de preguntas debe ser menos de 5 caracteres.")]
        public string choiceSet { get; set; }
    }
}
