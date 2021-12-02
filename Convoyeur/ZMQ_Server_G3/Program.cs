using APIs;
using Models;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
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
            fnc.Add("TurnON", x => api.CheckState() ? "false":api.turnON().ToString());
            fnc.Add("TurnOFF", x => !api.CheckState() ? "false" : api.turnOFF().ToString());
            fnc.Add("CheckStatus", x => api.CheckState().ToString());
            fnc.Add("GetBultosQuantityOnPile", x => api.getBultosQuantity().ToString());
            fnc.Add("GetBultosQuantityOnConveyor", x => api.getBultosQuantityOnCinta().ToString());
            fnc.Add("PutBulto", x => {
                var list = api.getBultos();
                Bultos blt = list.First();
                api.bultosDelete(blt.IDBultoMongo);
                //var bulto = x.Split('#');
                //blt.IDBulto = Int32.Parse(bulto[0]);
                //blt.IDBultoMongo = bulto[1];
                if (api.CheckState())
                {
                    api.addBultoToCinta(blt);
                    return "true";
                }
                else { return "false"; }
            });
            //asd.Invoke();
            #endregion

            #region TCP Listener
            //bool changePort = false;
            while (true)
            {
                try
                {
                    using (var server = new ResponseSocket("tcp://localhost:27022"))
                    {
                        //Once the server receives the Bulto from the Client it's makes the updates
                        // to the database to let the other component's know about it.
                        string requestIncoming = server.ReceiveFrameString();

                        String response = fnc[requestIncoming](requestIncoming);

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
                catch (Exception)
                { 
                    
                }
            }
            #endregion
        }
    }
}