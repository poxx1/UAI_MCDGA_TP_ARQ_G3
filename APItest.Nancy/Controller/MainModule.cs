using Nancy;
using Nancy.Extensions;
using Properties;
using APIs;
using System;

namespace Server.Nancy.Controller
{
    public class MainController : NancyModule
    {
        #region Variables & Instances
        private static bool cintaIsStarted = false;
        private static bool brazoIsStarted = false;
        private static bool prensaIsStarted = false;
        #endregion  
        public MainController()
        {
            #region Tests + Status 
            //Check the status of the server
            Get("/v1/status", _ =>
            {
                Console.WriteLine("Incoming request for /v1/status");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Request http code: 200 OK \r\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                return "GET 200: OK";
            });
            //Check POST method
            Post("/v1/test", _ =>
            {
                Console.WriteLine("Incoming request for /v1/test");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Request http code: 200 OK \r\n");
                Console.ForegroundColor = ConsoleColor.Cyan;

                var mongo = new MongoBDConnector();
                
                return mongo.connect() == true ? "POST 200 OK" : "POST 5xx ERROR";
            });
            #endregion

            #region Cinta
            Post("/v1/Info", x =>
            {
                //Objeto a modificar con el POST
                var product = new CuerpoCeleste();
                //Obtengo el raw
                var rawRequest = Context.Request.Body.AsString();
                //Lo paso a la clase

                var instancia = new Randomizer();

                if (cintaIsStarted)
                {
                    var get = instancia.calcularOutput();
                    var output = instancia.InfoCalculator(get);

                    return output;
                }

                else
                {
                    return "No esta iniciado el telescopio";
                }


            });

            Get("/v1/cinta/state", x=>
            {;


                return "?";
            });

            Get("/v1/cinta/sacar_bulto", x =>
            {;
                if (!cintaIsStarted)
                    return "First you have to turn ON the cinta";

                //Call to the sacarbultos service

                return "The bulto has been removed succesfully";
            });

            Get("/v1/cinta/poner_bulto", x =>
            {
                ;
                if (!cintaIsStarted)
                    return "First you have to turn ON the cinta";

                //Call to the ponerBulto service

                return "?";
            });

            Get("/v1/cinta/descontar_bulto", x =>
            {;


                return "?";
            });

            Get("/v1/cinta/turnon", x =>
            {;
                if (cintaIsStarted)
                {
                    Console.WriteLine("Incoming request for /v1/cinta_turnon");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Request http code: 200 OK - The cinta is already turned on \r\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    return "The cinta xd is already working";
                }
                cintaIsStarted = true;

                Console.WriteLine("Incoming request for /v1/cinta_turnon");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Request http code: 200 OK - The cinta is now ON\r\n");
                Console.ForegroundColor = ConsoleColor.Cyan;

                return "The cinta is now ON";

            });

            Get("/v1/cinta/turnoff", x =>
            {
                if (cintaIsStarted)
                {
                    cintaIsStarted = false;

                    Console.WriteLine("Incoming request for /v1/cinta_turnoff");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Request http code: 200 OK - The cinta turned off correctly \r\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    return "The cinta turned off correctly";
                }

                Console.WriteLine("Incoming request for /v1/cinta_turnoff");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Request http code: 200 OK - The cinta is already off\r\n");
                Console.ForegroundColor = ConsoleColor.Cyan;

                return "The cinta is already off";
            });
            #endregion
        }
    }
}