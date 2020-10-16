using System.ComponentModel.DataAnnotations;

namespace BlazorFormDesigner.Web.Requests
{
    public class LoginRequest
    {
        /// <summary>
        /// Unique username or e-mail address
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
