using MongoDB.Driver;
using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIs
{
    public class MongoDBConnector
    {
        #region BD Connection
        public IMongoDatabase connect()
        {
            var settings = MongoClientSettings.FromConnectionString(
                "mongodb://admin:xq3eKKtRrs2CApC@g3cluster-shard-00-00.ro39n.mongodb.net:27017," +
                "g3cluster-shard-00-01.ro39n.mongodb.net:27017," +
                "g3cluster-shard-00-02.ro39n.mongodb.net:27017/myFirstDatabase?" +
                "ssl=true&replicaSet=atlas-g88vwx-shard-0&authSource=admin&retryWrites=true&w=majority");

            var client = new MongoClient(settings);
            var database = client.GetDatabase("TP_ARQ");

            return database;
        }
        #endregion

        #region Prensa Manager

        public bool UpdatePress(bool isStarted, int idBulto)
        {
            var database = connect();
            var press = new Press() { IsStarted = isStarted, IdBulto = idBulto };
            var collection = database.GetCollection<Press>("Press");

            collection.ReplaceOne(p => p.IdBulto == idBulto, press);

            return true;
        }
        //public bool Delete(string mongoId)
        //{
        //    var database = connect();
        //    var collection = database.GetCollection<Prensa>("Bultos");

        //    collection.DeleteOne(b => b.IDBultoMongo == mongoId);

        //    return true;
        //}
        public List<Sensors> getSensores()
        {
            var database = connect();
            var collection = database.GetCollection<Sensors>("Bultos");
            List<Sensors> lst = collection.Find(p => true).ToList();

            return lst;
        }
        #endregion
    }
}