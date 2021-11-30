using MongoDB.Driver;
using Models;
using System.Collections.Generic;

namespace APIs
{
    public class ConeyourManagerAPI
    {
        MongoBD mongo = new MongoBD();
        Conveyour conv = new Conveyour();
        //Bultos bulto = new Bultos();

        #region Cinta Manager
        public bool addBultoToCinta(Bultos bulto)
        {
            List<Bultos> lst = new List<Bultos>();
            lst.Add(bulto);
            conv.ListBultos = lst;

            var database = mongo.connect();
            var collection = database.GetCollection<Conveyour>("Conveyour");
            collection.InsertOne(conv);

            return true;
        }
        public bool removeBultoFromCinta(string mongoId)
        {
            //This method is called from the Brazo;
            return true;
        }
        public List<Bultos> getBultosOnCinta()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Bultos>("Cinta");
            List<Bultos> lst = collection.Find(b => true).ToList();

            return lst;
        }
        public int getBultosQuantityOnCinta()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Bultos>("Cinta");

            List<Bultos> lst = collection.Find(b => true).ToList();

            return lst.Count;
        }
        #endregion

        #region Bultos Manager
        public bool bultosDelete(string mongoId)
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Bultos>("Bultos");

            collection.DeleteOne(b => b.IDBultoMongo == mongoId);

            return true;
        }
        public List<Bultos> getBultos()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Bultos>("Bultos");
            var lst = collection.Find(b => true).ToList(); //List<Bultos>

            return lst;
        }
        public int getBultosQuantity()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Bultos>("Bultos");

            List<Bultos> lst = collection.Find(b => true).ToList();

            return lst.Count;
        }
        #endregion
    }
}