using System;
using Microsoft.Owin.Hosting;

namespace APItest.Nancy
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Startup UI
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                                                                                                                          ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("                                  MCDGA - UAI - Trabajo practico de Arquitectura - Grupo 3                                ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                                                                                                                          \r\n\r\n");
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
                Console.WriteLine($"Port: {url}");
                Console.ReadLine(); //Espero 
            }
        }
    }
}
