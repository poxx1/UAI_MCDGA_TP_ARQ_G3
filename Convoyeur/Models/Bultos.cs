using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Bultos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IDBultoMongo { get; set; }

        [BsonElement("IDBulto")]
        public int IDBulto { get; set; }
    }
}