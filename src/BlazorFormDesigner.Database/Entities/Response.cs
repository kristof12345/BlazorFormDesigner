using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BlazorFormDesigner.Database.Entities
{
    public class Response
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FormId { get; set; }

        public string UserId { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
