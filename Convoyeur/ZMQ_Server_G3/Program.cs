using APIs;
using Models;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ZMQ_Server_G3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Server side
            Console.WriteLine("Server");
            ConeyourManagerAPI api = new ConeyourManagerAPI();
            Bultos blt = new Bultos();

            #region Decision tree
            //Dictionary<string,string> dic = new Dictionary<string,string>();
            Dictionary<string,Func<string,string>> fnc = new Dictionary<string, Func<string,string>>();
            fnc.Add("turnon", x => api.CheckState() ? "false":api.turnON().ToString());
            fnc.Add("turnoff", x => !api.CheckState() ? "false" : api.turnOFF().ToString());
            fnc.Add("checkstatus", x => api.CheckState().ToString());
            fnc.Add("GetBultosQuantityOnPile", x => api.getBultosQuantity().ToString());
            fnc.Add("GetBultosQuantityOnConveyor", x => api.getBultosQuantityOnCinta().ToString());
            fnc.Add("PutBulto", x => {
                var bulto = x.Split('#');
                blt.IDBulto = Int32.Parse(bulto[0]);
                blt.IDBultoMongo = bulto[1];
                if (api.CheckState())
                {
                    api.addBultoToCinta(blt);
                    return "ok";
                }
                else { return "Can't add since the Conveyor is OFF. Please first turn ON."; }
            });
            //asd.Invoke();
            #endregion

            #region TCP Listener
            //bool changePort = false;

            while (true)
            {
                    using (var server = new ResponseSocket("tcp://localhost:27022"))
                    {
                        //Once the server receives the Bulto from the Client it's makes the updates
                        // to the database to let the other component's know about it.
                        string requestIncoming = server.ReceiveFrameString();
  
                        String response = fnc[requestIncoming](requestIncoming);    

                        //Here i call the methods for the API
                        //Add bulto to the List of Bultos in the Cinta.
                        //Parses the response
                        
                        server.SendFrame(response);
                        Console.WriteLine("Response sent");
                        Thread.Sleep(3000);
                        //changePort = false;
                        //Console.ReadLine();
                        server.Close();
                        //server.Disconnect("tcp://localhost:27022");
                        server.Dispose();
                        //server.Unbind("tcp://localhost:27022");
                    }
            }
            #endregion
        }
    }
}