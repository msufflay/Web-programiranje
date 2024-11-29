using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class PostCreateRequests
    {
        [Required(ErrorMessage = "Naslov je obavezan")]
        [StringLength(20, ErrorMessage = "Naslov ne smije biti duže od 20 znakova")]
        public string Title { get; set; }
        [Required(ErrorMessage = "User Id je obavezan")]
        public string UserId { get; set; }

    }
}
