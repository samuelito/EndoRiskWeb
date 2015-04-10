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
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        public string email { get; set; }

        [Display(Name = " Password ")]
        [Required(ErrorMessage = "A password is required", AllowEmptyStrings=false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(25, ErrorMessage = "Password can't be longer than 25 characters.")]
        public string password { get; set; }

        [Display(Name = " First Name ")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters.")]
        public string firstname { get; set; }

        [Display(Name = " Last Name ")]
        [StringLength(50, ErrorMessage = "Lastname can't be longer than 50 characters.")]
        public string lastname { get; set; }
        [Display(Name = " SubAdmin ")]
        public long subadmin { get; set; }
    }
}
