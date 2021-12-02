using Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace APIs
{
    public class PressAPI
    {
        MongoDBConnector mongo = new MongoDBConnector();

        public bool TurnOn()
        {
            if (!CurrentState())
            {
                var database = mongo.connect();
                var press = new Press() { IsStarted = true, IdBulto = -1, IdPrensa = GetPrensa() };
                var collection = database.GetCollection<Press>("Press");

                collection.ReplaceOne(p => p.IdPrensa == GetPrensa(), press);

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TurnOff()
        {
            if (CurrentState())
            {
                var database = mongo.connect();
                var press = new Press() { IsStarted = false, IdBulto = -1, IdPrensa = GetPrensa() };
                var collection = database.GetCollection<Press>("Press");

                collection.ReplaceOne(p => p.IdPrensa == GetPrensa(), press);

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CurrentState()
        {
            var db = mongo.connect();
            var collection = db.GetCollection<Press>("Press");
            List<Press> lst = collection.Find(p => true).ToList();

            return lst.First().IsStarted;
        }
        public string GetPrensa()
        {
            var db = mongo.connect();
            var collection = db.GetCollection<Press>("Press");
            List<Press> lst = collection.Find(p => true).ToList();

            return lst.First().IdPrensa;
        }
        public bool Create()
        {
            var database = mongo.connect();
            var press = new Press() { IsStarted = false, IdBulto = -1 };
            var collection = database.GetCollection<Press>("Press");

            collection.InsertOne(press);

            return true;
        }
    }
}