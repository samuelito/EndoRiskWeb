using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class preExistingConditions
    {     
            public int idPreCond { get; set; }
            [Display(Name = "Condición Pre-existente")]
            [Required]
            [StringLength(255, ErrorMessage = "Title can't be longer than 255 characters.")]
            public string condition { get; set; }
            [Display(Name = "Abreviatura")]
            [Required]
            [StringLength(5, ErrorMessage = "Title can't be longer than 5 characters.")]
            public string abbr { get; set; }        
    }
}