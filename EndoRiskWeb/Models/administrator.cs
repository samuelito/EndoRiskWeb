using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndoRiskWeb.Models
{
    public partial class administrator
    {
        public long idAdmin { get; set; }

        [Display(Name = "Email address ")]
        [Required(ErrorMessage = "Email address is required", AllowEmptyStrings=false)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Display(Name = " Password ")]
        [Required(ErrorMessage = "A password is required", AllowEmptyStrings=false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string password { get; set; }

        [Display(Name = " First Name ")]
        public string firstname { get; set; }

        [Display(Name = " Last Name ")]
        public string lastname { get; set; }
        [Display(Name = " SubAdmin ")]
        public long subadmin { get; set; }
    }
}
