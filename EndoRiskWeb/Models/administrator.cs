using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class administrator
    {
        public long idAdmin { get; set; }

        [Display(Name = "Correo electrónico ")]
        [Required(ErrorMessage = "Correo electrónico requerido", AllowEmptyStrings=false)]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede contener más de 100 carateres.")]
        public string email { get; set; }

        [Display(Name = " Contraseña ")]
        [Required(ErrorMessage = "Contraseña requerida", AllowEmptyStrings=false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(255, ErrorMessage = "La contraseña no puede contener más de 255 carateres.")]
        public string password { get; set; }
        
        [Display(Name = " Nombre ")]
        [Required(ErrorMessage = "Nombre requerido", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "El nombre no puede contener más de 50 carateres.")]
        public string firstname { get; set; }

        [Display(Name = " Apellido ")]
        [Required(ErrorMessage = "Apellido requerido", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "El apellido no puede contener más de 50 carateres.")]
        public string lastname { get; set; }

        [Display(Name = " Sub-Administador ")]
        public long subadmin { get; set; }

        public string passwordSalt { get; set; }

    }
}
