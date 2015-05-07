using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class romediagnosi
    {
        public int idDiagnosis { get; set; }

        [Display(Name = "Enfermedad ")]
        [Required(ErrorMessage = "No puede dejar la enfemedad en blanco", AllowEmptyStrings = false)]
        [StringLength(255, ErrorMessage = "La enfermedad no debe tener mas de 255 carateres.")]
        public string disease { get; set; }

        [Display(Name = " Diagnostico ")]
        [Required(ErrorMessage = "No puede dejar el diagnostico en blanco", AllowEmptyStrings = false)]
        [StringLength(5000, ErrorMessage = "El diagnostico debe tener un maximo de 5,000 carateres.")]
        public string diagnosis { get; set; }

        [Display(Name = " Cuestionario ")]
        [Required(ErrorMessage = "Indique al cuestionario que pertenece.", AllowEmptyStrings = false)]
        [StringLength(45, ErrorMessage = "El nombre del cuestionario no puede contener mas de 45 carateres.")]
        public string rome { get; set; }
    }
}
