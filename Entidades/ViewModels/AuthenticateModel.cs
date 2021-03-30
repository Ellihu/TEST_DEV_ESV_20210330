using System.ComponentModel.DataAnnotations;

namespace Entidades.ViewModels
{
    public class AutenticarVModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

    }

  

}
