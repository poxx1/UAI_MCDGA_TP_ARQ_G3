using Models;
using MongoDB.Driver;
using Observer;
using System.Collections.Generic;
using System.Linq;

namespace APIs
{
    public class SensorsAPI : IObserver
    {
        MongoDBConnector mongo = new MongoDBConnector();
        public bool CreateActive()
        {
            var database = mongo.connect();
            var sensor = new Sensors() { Name = "Active", State = true };
            var collection = database.GetCollection<Sensors>("Sensors");

            collection.InsertOne(sensor);

            return true;
        }
        public bool CreatePassive()
        {
            var database = mongo.connect();
            var sensor = new Sensors() { Name = "Passive", State = true };
            var collection = database.GetCollection<Sensors>("Sensors");

            collection.InsertOne(sensor);

            return true;
        }
        //DOWN = TRUE;
        //UP = FALSE;
        public bool ArmUP()
        {
            //Last to check if the arm is already UP STATE = FALSE

            if (!CheckActive())
            {
                var db = mongo.connect();
                var collection = db.GetCollection<Sensors>("Sensors");
                var sensor = new Sensors() { Name = "Active", State = false, IDSensor = GetActive() };

                //InsertIntoMongo
                collection.ReplaceOne(s => s.IDSensor == GetActive(), sensor);
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool ArmDOWN()
        {
            //Last to check if the arm is already DOWN STATE = TRUE
            if (CheckActive())
            {
                var db = mongo.connect();
                var collection = db.GetCollection<Sensors>("Sensors");
                var sensor = new Sensors() { Name = "Active", State = true, IDSensor = GetActive() };

                //InsertIntoMongo
                collection.ReplaceOne(s => s.IDSensor == GetActive(), sensor);
                return true;
            }
            else
            {
                return false;
            }
        }

        //IS RELEASED == FALSE
        //IS OCUPPED == TRUE
        public bool RelasePress()
        {
            if (CheckPassive())
            {
                var db = mongo.connect();
                var collection = db.GetCollection<Sensors>("Sensors");
                var sensor = new Sensors() { Name = "Pasive", State = false, IDSensor = GetPassive() };

                //InsertIntoMongo
                collection.ReplaceOne(s => s.IDSensor == GetPassive(), sensor);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool OcupatePress()
        {
            if (!CheckPassive())
            {
                var db = mongo.connect();
                var collection = db.GetCollection<Sensors>("Sensors");
                var sensor = new Sensors() { Name = "Passive", State = true, IDSensor = GetPassive() };

                //InsertIntoMongo
                collection.ReplaceOne(s => s.IDSensor == GetPassive(), sensor);
                return true;
            }
            else
            {
                return false;
            }

        }
        public string GetActive()
        {
            var db = mongo.connect();
            var collection = db.GetCollection<Sensors>("Sensors");
            List<Sensors> lst = collection.Find(p => true).ToList();

            return lst.First().IDSensor;
        }
        public string GetPassive()
        {
            var db = mongo.connect();
            var collection = db.GetCollection<Sensors>("Sensors");
            List<Sensors> lst = collection.Find(p => true).ToList();

            return lst.First().IDSensor;
        }
        public bool CheckActive()
        {
            var db = mongo.connect();
            var collection = db.GetCollection<Sensors>("Sensors");
            List<Sensors> lst = collection.Find(p => true).ToList();

            return lst.First().State;
        }
        public bool CheckPassive()
        {
            var db = mongo.connect();
            var collection = db.GetCollection<Sensors>("Sensors");
            List<Sensors> lst = collection.Find(p => true).ToList();

            return lst.First().State;
        }
        public bool Update(ISubject subject)
        {
            //Levanto el brazo
            if (!CheckActive())
            {
                var db = mongo.connect();
                var collection = db.GetCollection<Sensors>("Sensors");
                var sensor = new Sensors() { Name = "Active", State = false, IDSensor = GetActive() };

                //InsertIntoMongo
                collection.ReplaceOne(s => s.IDSensor == GetActive(), sensor);
                return true;
            }
            else
            {
                return false;
            }
        }
        void IObserver.Update(ISubject subject)
        {
            throw new System.NotImplementedException();
        }
    }
}