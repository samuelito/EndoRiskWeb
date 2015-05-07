using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class endochoice
    {
        public int idChoice { get; set; }
        [Display(Name="Set")]
        [StringLength(5, ErrorMessage = "Set can't be longer than 5 characters.")]
        public string choiceSet { get; set; }
        [Display(Name="Opcion del Set")]
        [StringLength(45, ErrorMessage = "Title can't be longer than 45 characters.")]
        public string choiceOption { get; set; }
    }
}
