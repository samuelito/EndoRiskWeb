using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class comment
    {
        public long idComment { get; set; }

        [StringLength(44, ErrorMessage = "Title can't be longer than 44 characters.")]
        public string title { get; set; }
        [StringLength(255, ErrorMessage = "Content can't be longer than 255 characters.")]
        public string content { get; set; }
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        public string email { get; set; }
        public Nullable<System.DateTime> time { get; set; }
    }
}
