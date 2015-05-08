using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndoRiskWeb.Models
{
    //public class UsersContext : DbContext
    //{
    //    public UsersContext()
    //        : base("DefaultConnection")
    //    {
    //    }

    //    public DbSet<UserProfile> UserProfiles { get; set; }
    //}

    //[Table("UserProfile")]
    //public class UserProfile
    //{
    //    [Key]
    //    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    public int UserId { get; set; }
    //    public string UserName { get; set; }
    //}

    //public class RegisterExternalLoginModel
    //{
    //    [Required]
    //    [Display(Name = "User name")]
    //    public string UserName { get; set; }

    //    public string ExternalLoginData { get; set; }
    //}

    public class LocalPasswordModel
    {       
        //[DataType(DataType.EmailAddress)] 
        [Display(Name = "Correo electrónico")]
        public string userEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(25, ErrorMessage = "La contraseña no puede contener más de 25 carateres.")]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe contener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme su contraseña")]
        [Compare("NewPassword", ErrorMessage = "Su contraseña no es igual a la escrita en el campo anterior.")]
        public string ConfirmPasswordChange { get; set; }
    }

    public class LoginModel
    {
        [Display(Name = "Correo electrónico ")]
        [Required(ErrorMessage = "Correo electrónico requerido", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede contener más de 100 carateres.")]
        public string email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe contener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        //[Display(Name = "Recuérdame")]
        //public bool RememberMe { get; set; }   
    }

    public class RegisterModel
    {
        [Display(Name = " Nombre ")]
        [Required(ErrorMessage = "Por favor introduzca su nombre", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "El nombre no puede contener más de 50 carateres.")]
        public string firstname { get; set; }

        [Display(Name = " Apellido ")]
        [Required(ErrorMessage = "Por favor introduzca su apellido", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "El apellido no puede contener más de 50 carateres.")]
        public string lastname { get; set; }

        [Display(Name = " Sub-Administador ")]
        public long subadmin { get; set; }

        [Display(Name = "Correo electrónico ")]
        [Required(ErrorMessage = "Correo electrónico requerido", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede contener más de 100 carateres.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Contraseña requerida", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "La {0} debe contener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Por favor, introduzca su contraseña nuevamente", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Su contraseña no es igual a la escrita en el campo anterior.")]
        public string ConfirmPasswordRegister { get; set; }
    }
}
