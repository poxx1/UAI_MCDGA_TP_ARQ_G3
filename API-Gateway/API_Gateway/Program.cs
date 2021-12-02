using System;
using Microsoft.Owin.Hosting;

namespace API_Gateway
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("API GATEWAY");

            string url = "http://+:27021";

            using (WebApp.Start<StartUp>(url))
            {
                Console.WriteLine("Corriendo....");
                Console.ReadLine();
            }
        }
    }
}