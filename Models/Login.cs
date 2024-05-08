using System.ComponentModel.DataAnnotations;

namespace GoogleReCaptcha.Models
{
    public class Login
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]  
        public string? Token { get; set;}
    }
}
