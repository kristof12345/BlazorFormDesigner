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
    }
}
