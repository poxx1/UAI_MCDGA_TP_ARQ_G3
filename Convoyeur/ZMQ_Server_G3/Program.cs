using APIs;
using Models;
using NetMQ;
using NetMQ.Sockets;
using System;

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

            using (var server = new ResponseSocket("tcp://localhost:27016"))
            {
                //Once the server receives the Bulto from the Client it's makes the updates
                // to the database to let the other component's know about it.

                string bultoReceived = server.ReceiveFrameString();
                String response = "Bulto received";

                //Here i call the methods for the API
                //Add bulto to the List of Bultos in the Cinta.
                //Parses the response
                var bulto = bultoReceived.Split('#');

                blt.IDBulto = Int32.Parse(bulto[0]);
                blt.IDBultoMongo = bulto[1];
                api.addBultoToCinta(blt);

                server.SendFrame(response);
                Console.WriteLine("Response sent");
                Console.ReadLine();
            }
        }
    }
}