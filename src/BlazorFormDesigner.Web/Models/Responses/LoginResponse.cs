using BlazorFormDesigner.Web.Models;

namespace BlazorFormDesigner.Web.Responses
{
    public class LoginResponse
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        internal User ToUser()
        {
            return new User
            {
                Name = Name,
                Token = Token,
                Username = Username
            };
        }
    }
}