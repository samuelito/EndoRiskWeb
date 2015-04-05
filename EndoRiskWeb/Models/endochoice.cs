using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class endochoice
    {
        public int idChoice { get; set; }
        [Display(Name="Set")]
        public string choiceSet { get; set; }
        [Display(Name="Option")]
        public string choiceOption { get; set; }
    }
}
