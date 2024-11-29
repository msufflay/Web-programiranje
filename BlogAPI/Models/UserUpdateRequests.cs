using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class UserUpdateRequests
    {
        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(20, ErrorMessage = "Ime ne smije biti duže od 20 znakova")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email je obavezan")]
        [StringLength(20, ErrorMessage = "Ime ne smije biti duže od 20 znakova")]
        public string Email { get; set; }
    }
}
