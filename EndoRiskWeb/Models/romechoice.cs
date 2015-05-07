using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class romechoice
    {
        public long idRomeChoices { get; set; }

        [Display(Name = "Set ")]
        [Required(ErrorMessage = "No puede dejar el set en blanco", AllowEmptyStrings = false)]
        [StringLength(45, ErrorMessage = "El set no puede contener mas de 45 carateres.")]
        public string romeChoice1 { get; set; }

        [Display(Name = " Opcion ")]
        [Required(ErrorMessage = "Debe proveer una opcion para este set", AllowEmptyStrings = false)]
        [StringLength(255, ErrorMessage = "La opcion no debe contener mas de 255 carateres.")]

        public string romeOption { get; set; }

        [Display(Name = " Valor ")]
        [Required(ErrorMessage = "No puede dejar el campo en blanco. Debe tener un valor numerico")]
        public Nullable<int> value { get; set; }
    }
}
