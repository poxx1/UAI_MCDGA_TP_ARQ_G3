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

            using (var server = new ResponseSocket("tcp://localhost:27016"))
            {
                server.ReceiveFrameString();
                String response = "Bulto recieved";
                server.SendFrame(response);
                Console.WriteLine("Response sent");
                Console.ReadLine();
            }
        }
    }
}