using System.ComponentModel.DataAnnotations;

namespace BlazorFormDesigner.Web.Models
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
