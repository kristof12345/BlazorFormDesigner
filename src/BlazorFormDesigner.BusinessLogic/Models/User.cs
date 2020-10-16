namespace BlazorFormDesigner.BusinessLogic.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public byte[] Salt { get; set; }

        public string Name { get; set; }
    }
}
