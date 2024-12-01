using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class CommentCreateRequests
    {
        [Required(ErrorMessage = "Komentar je obavezan")]
        [StringLength(20, ErrorMessage = "Komentar ne smije biti duže od 100 znakova")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Id posta koji komentirate je obavezan")]
        public string PostId { get; set; }
    }
}
