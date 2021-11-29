using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Models
{
    public class Press
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdPrensa { get; set; }
        public bool IsStarted { get; set; }
        public int IdBulto { get; set; }
    }
}