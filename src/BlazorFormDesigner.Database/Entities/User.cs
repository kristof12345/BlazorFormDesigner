using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorFormDesigner.Database.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Username { get; set; }

        public string Password { get; set; }

        public byte[] Salt { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<string> AnsweredForms { get; set; }

        public List<string> DismissedForms { get; set; }

        public List<string> CreatedForms { get; set; }
    }
}
