using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Models
{
    public class Press//:Machines
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdPress { get; set; }
        public bool IsStarted { get; set; }
        public List<Bultos> ListBultos { get; set; }
    }
}