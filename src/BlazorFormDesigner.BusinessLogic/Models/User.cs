using System.Collections.Generic;

namespace BlazorFormDesigner.BusinessLogic.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public byte[] Salt { get; set; }

        public string Name { get; set; }

        public List<string> AnsweredForms { get; set; }

        public List<string> DismissedForms { get; set; }

        public List<string> CreatedForms { get; set; }
    }
}
