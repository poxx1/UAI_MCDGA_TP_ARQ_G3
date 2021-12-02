using MongoDB.Driver;
using Models;
using System.Collections.Generic;
using System.Linq;

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
            
            var database = mongo.connect();
            var collection = database.GetCollection<Conveyour>("Conveyour");

            List<Conveyour> lstB = collection.Find(b => true).ToList();

            conv = lstB.First();

            foreach (var item in lstB.ElementAt(0).ListBultos)
            {
                lst.Add(item);
            }

            conv.ListBultos = lst;

            collection.ReplaceOne(c => c.IdConveyourMongo == conv.IdConveyourMongo, conv);

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
        public bool turnON()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Conveyour>("Conveyour");
            List<Conveyour> lst = collection.Find(b => true).ToList();

            conv = lst.First();

            conv.IsStarted = true;

            collection.ReplaceOne(c => c.IdConveyourMongo == conv.IdConveyourMongo, conv);

            return true;
        }
        public bool turnOFF()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Conveyour>("Conveyour");
            List<Conveyour> lst = collection.Find(b => true).ToList();

            conv = lst.First();

            conv.IsStarted = false;

            collection.ReplaceOne(c => c.IdConveyourMongo == conv.IdConveyourMongo, conv);

            return true;
        }
        public bool CheckState()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Conveyour>("Conveyour");
            List<Conveyour> lst = collection.Find(b => true).ToList();

            //turnON();

            return lst.First().IsStarted;
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