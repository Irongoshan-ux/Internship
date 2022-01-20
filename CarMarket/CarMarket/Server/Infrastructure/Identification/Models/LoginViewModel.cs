using System.ComponentModel.DataAnnotations;

namespace CarMarket.Server.Infrastructure.Identification.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}