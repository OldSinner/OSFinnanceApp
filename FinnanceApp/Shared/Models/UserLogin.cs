using System.ComponentModel.DataAnnotations;

namespace FinnanceApp.Shared.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Proszę podać adres email.")]
        [EmailAddress(ErrorMessage = "Proszę podać poprawny adres email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Proszę podać hasło.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}