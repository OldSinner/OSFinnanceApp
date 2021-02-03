using System.ComponentModel.DataAnnotations;

namespace FinnanceApp.Shared.Models
{
    public class EditProfile
    {
        [Required(ErrorMessage = "Nazwa Użytkownika jest wymagana")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa użytkownika jest za krótka")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Adres Email jest niepoprawny")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Hasło musi mieć minimum 6 znaków")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Cel jest wymagany")]
        [Range(0,100000)]
        public double TargetValue { get; set; }

    }
}