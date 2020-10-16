using BlazorFormDesigner.BusinessLogic.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BlazorFormDesigner.Database.Entities
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Title { get; set; }

        public string Description { get; set; }

        public QuestionType Type { get; set; }

        public bool IsCorrected { get; set; }

        public List<Option> Options { get; set; }
    }
}
