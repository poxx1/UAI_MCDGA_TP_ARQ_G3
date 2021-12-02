using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Models
{
    public class Sensors
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IDSensor { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }
    }
}
