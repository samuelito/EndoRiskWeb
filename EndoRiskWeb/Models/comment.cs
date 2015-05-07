using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class comment
    {
        public long idComment { get; set; }

        [Display(Name = "Título ")]
        [StringLength(44, ErrorMessage = "Title can't be longer than 44 characters.")]
        public string title { get; set; }
        [Display(Name = "Contenido ")]
        [DataType(DataType.MultilineText)]
        [StringLength(4900, ErrorMessage = "Content can't be longer than 4900 characters.")]
        public string content { get; set; }
        [Display(Name = "Email ")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        public string email { get; set; }
        [Display(Name = "Hora ")]
        public Nullable<System.DateTime> time { get; set; }
    }
}
