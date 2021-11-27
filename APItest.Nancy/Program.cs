using System;
using System.Threading;
using Microsoft.Owin.Hosting;

namespace Server.Nancy
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Startup UI
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("                                                                                                                        ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("                                MCDGA - UAI - Trabajo practico de Arquitectura - Grupo 3                                ");
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("                                                                                                                        \r\n\r\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            #endregion

            //Configuro el puerto
            var url = "http://+:27015";
            //Inicializo el servicio

            using (WebApp.Start<StartUp>(url))
            {
                //Valido si existe el servicio/se inicializo
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Server is up and running...");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Port: {url}\r\n");

                Console.ReadLine(); //Espero 
            }
        }
    }
}
