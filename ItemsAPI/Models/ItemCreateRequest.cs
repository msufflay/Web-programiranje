using System.ComponentModel.DataAnnotations;

namespace ItemsAPI.Models
{
    public class ItemCreateRequest
    {
        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(20, ErrorMessage = "Naziv ne smije biti duži od 20 znakova")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Opis je obavezan")]
        [StringLength(30, ErrorMessage = "Opis ne smije biti duži od 30 znakova")]
        public string Description { get; set; }
    }
}
