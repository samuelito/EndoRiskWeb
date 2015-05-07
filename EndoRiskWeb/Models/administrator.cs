using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class administrator
    {
        public long idAdmin { get; set; }

        [Display(Name = "Correo electr�nico ")]
        [Required(ErrorMessage = "Correo electr�nico requerido", AllowEmptyStrings=false)]
        [EmailAddress(ErrorMessage = "Correo electr�nico inv�lido")]
        [StringLength(100, ErrorMessage = "El correo electr�nico no puede contener m�s de 100 carateres.")]
        public string email { get; set; }

        [Display(Name = " Contrase�a ")]
        [Required(ErrorMessage = "Contrase�a requerida", AllowEmptyStrings=false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(255, ErrorMessage = "La contrase�a no puede contener m�s de 255 carateres.")]
        public string password { get; set; }
        
        [Display(Name = " Nombre ")]
        [Required(ErrorMessage = "Nombre requerido", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "El nombre no puede contener m�s de 50 carateres.")]
        public string firstname { get; set; }

        [Display(Name = " Apellido ")]
        [Required(ErrorMessage = "Apellido requerido", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "El apellido no puede contener m�s de 50 carateres.")]
        public string lastname { get; set; }

        [Display(Name = " Sub-Administador ")]
        public long subadmin { get; set; }

        public string passwordSalt { get; set; }

    }
}
