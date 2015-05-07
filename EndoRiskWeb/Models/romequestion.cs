using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class romequestion
    {
        public int idRomeQuestion { get; set; }

        [Display(Name = "Pregunta ")]
        [Required(ErrorMessage = "No puede dejar la pregunta en blanco", AllowEmptyStrings = false)]
        [StringLength(255, ErrorMessage = "La pregunta no puede contener mas de 255 carateres.")]
        public string romeQuestion1 { get; set; }

        [Display(Name = "Set de Opciones ")]
        [Required(ErrorMessage = "No puede dejar el set de la pregunta en blanco", AllowEmptyStrings = false)]
        [StringLength(45, ErrorMessage = "El set no puede contener mas de 45 carateres.")]
        public string romeChoice { get; set; }
    }
}
