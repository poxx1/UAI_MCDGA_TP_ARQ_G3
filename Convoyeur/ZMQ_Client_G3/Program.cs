using APIs;
using Models;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Linq;

namespace ZMQ_Client_G3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Server side
            Console.WriteLine("Client");
            ConeyourManagerAPI api = new ConeyourManagerAPI();

            using (var client = new RequestSocket("tcp://localhost:27022"))
            {
                //I send bultos to the Cinta using this ZeroMQ Client.
                //First i took one Bulto from the Pila of Bultos.
                var list = api.getBultos();
                Bultos bulto = list.First();
                api.bultosDelete(bulto.IDBultoMongo);

                //Then i send this bulto to the Server that is the Cinta
                String request = bulto.IDBulto + "#" + bulto.IDBultoMongo;
                client.SendFrame(request);
                var serverReturn = client.ReceiveFrameString();
                Console.WriteLine(serverReturn);
            }
        }
    }
}