

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorFormDesigner.Database.Entities
{
    public class Option
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Content { get; set; }

        public bool IsCorrect { get; set; }
    }
}
