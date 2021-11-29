using MongoDB.Driver;
using Models;
using System.Collections.Generic;

namespace APIs
{
    public class MongoBD
    {
        #region Shared Methods
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

        #region Bultos Manager
        public bool bultosInsert(int id)
        {
            var database = connect();
            var bulto = new Bultos() { IDBulto = id };
            var collection = database.GetCollection<Bultos>("Bultos");

            collection.InsertOne(bulto);

            return true;
        }
        public bool bultosDelete(string mongoId)
        {
            var database = connect();
            var collection = database.GetCollection<Bultos>("Bultos");

            collection.DeleteOne(b => b.IDBultoMongo == mongoId);

            return true;
        }
        public List<Bultos> getBultos()
        {
            var database = connect();
            var collection = database.GetCollection<Bultos>("Bultos");
            List<Bultos> lst = collection.Find(b => true).ToList();

            return lst;
        }
        public int getBultosQuantity()
        {
            var database = connect();
            var collection = database.GetCollection<Bultos>("Bultos");

            List<Bultos> lst = collection.Find(b => true).ToList();

            return lst.Count;
        }
        #endregion

        #region Cinta Manager
        public bool addBultoToCinta(int id)
        {

            return true;
        }
        public bool removeBultoFromCinta(string mongoId)
        {

            return true;
        }
        public List<Bultos> getBultosOnCinta()
        {
            var database = connect();
            var collection = database.GetCollection<Bultos>("Cinta");
            List<Bultos> lst = collection.Find(b => true).ToList();

            return lst;
        }
        public int getBultosQuantityOnCinta()
        {
            var database = connect();
            var collection = database.GetCollection<Bultos>("Cinta");

            List<Bultos> lst = collection.Find(b => true).ToList();

            return lst.Count;
        }
        #endregion
    }
}