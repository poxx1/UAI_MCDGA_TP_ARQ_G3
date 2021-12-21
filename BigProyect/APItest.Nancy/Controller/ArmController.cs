using Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using APIs;

namespace Server.Nancy.Controller
{
    public class ArmController
    {
        MongoBD mongo = new MongoBD();
        Arm arm = new Arm();
        Conveyour conv = new Conveyour();

        //ALSO CHECKS IF THE CONVEYOR IS ON
        public bool PressIsFree()
        {
            if (!CurrentPressState() && !CheckPassive())
                return true;
            else
                return false;
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
        public bool CurrentPressState()
        {
            var db = mongo.connect();
            var collection = db.GetCollection<Press>("Press");
            List<Press> lst = collection.Find(p => true).ToList();

            return lst.First().IsStarted;
        }

        public bool AddPressToBD()
        {   
            var database = mongo.connect();
            var collection = database.GetCollection<Press>("Press");
            var press = new Press() { IsStarted = true };
            List<Press> lst = new List<Press>();//collection.Find(b => true).ToList();
            List<Bultos> lstB = new List<Bultos>();

            var itemN = new Bultos();
            itemN.IDBulto = 3;

            lstB.Add(itemN);

            press.ListBultos = lstB;

            collection.InsertOne(press);

            return true;
        }

        //Take bultos From cinta
        public bool TakeFromConveyor()
        {
            //Checks is Press is free
            if(PressIsFree())
            {
                //Delete bulto from Conveyor
                Press press = new Press();
                List<Bultos> lst = new List<Bultos>();
                var bulto = new Bultos();
                var database = mongo.connect();
                var collection = database.GetCollection<Conveyour>("Conveyour");

                //Brings the bulto to a List
                List<Conveyour> lstB = collection.Find(b => true).ToList();
                conv = lstB.First();

                lst = conv.ListBultos;
                bulto = lst.First();

                //Get the bulto from the array
                lst.Remove(lst.First());
                conv.ListBultos = lst;

                //Update the Conveyor list without the Bulto
                collection.ReplaceOne(c => c.IdConveyourMongo == conv.IdConveyourMongo, conv);

                List<Bultos> listBultosPress = new List<Bultos>();

                //Send to press
                if (SendToPress())
                {
                    var collectionP = database.GetCollection<Press>("Press");

                    //Brings the bulto to a List
                    List<Press> lstC = collectionP.Find(p => true).ToList();
                    press = lstC.First();
                    //Now foearch to add the Bulto to the list on the Press

                    foreach (var item in lstC.ElementAt(0).ListBultos)
                    {
                        listBultosPress.Add(item);
                    }

                    //Add the bulto to the list
                    listBultosPress.Add(bulto);

                    //Now passes all the bultos to the new list
                    press.ListBultos = listBultosPress;

                    //Replace the item on the Press with the new list
                    collectionP.ReplaceOne(f => f.IdPress == press.IdPress, press);

                    return true;
                }

                else 
                {
                    return false;
                }
                
            }
            return false;
        }

        //Pass the bulto to the Press
        public bool SendToPress()
        {
            //First checks if the press is OK
            var database = mongo.connect();
            var collection = database.GetCollection<Press>("Press");

            return true;
        }
        public bool CheckState()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Arm>("Arm");
            List<Arm> lst = collection.Find(a => true).ToList();

            arm = lst.First();
            
            return arm.IsStarted;
        }
        public bool TurnON()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Arm>("Arm");
            List<Arm> lst = collection.Find(a => true).ToList();

            arm = lst.First();
            arm.IsStarted = true;

            collection.ReplaceOne(c => c.IdArmMongo == arm.IdArmMongo, arm);

            return true;
        }
        public bool TurnOFF()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Arm>("Arm");
            List<Arm> lst = collection.Find(a => true).ToList();

            arm = lst.First();
            arm.IsStarted = true;

            collection.ReplaceOne(c => c.IdArmMongo == arm.IdArmMongo, arm);

            return false;
        }

        public bool CreateArm()
        {
            var database = mongo.connect();
            var collection = database.GetCollection<Arm>("Arm");
            var arm = new Arm() { IsStarted = true };
            collection.InsertOne(arm);

            return true;
        }
    }
}