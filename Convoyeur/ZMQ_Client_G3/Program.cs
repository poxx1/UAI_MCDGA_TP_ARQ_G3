using NetMQ;
using NetMQ.Sockets;
using System;

namespace ZMQ_Client_G3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Server side
            Console.WriteLine("Client");

            using (var client = new RequestSocket("tcp://localhost:27016"))
            {
                String request = "Bulto";
                client.SendFrame(request);
                var serverReturn = client.ReceiveFrameString();
                Console.WriteLine(serverReturn);
                Console.ReadLine();
            }
        }
    }
}