using System.ComponentModel.DataAnnotations;

namespace SnacksUdemy.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Inform The name")]
        [Display(Name="User")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Inform The password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
