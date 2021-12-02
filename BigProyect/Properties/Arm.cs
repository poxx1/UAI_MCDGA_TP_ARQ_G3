using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Arm
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdConveyourMongo { get; set; }
        public bool IsStarted { get; set; }
        public List<Bultos> ListBultos { get; set; }
    }
}