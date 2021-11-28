using Nancy;
using Models;
using Nancy.Extensions;
using Properties;
using APIs;
using System;
using System.Collections.Generic;

namespace Server.Nancy.Controller
{
    public class MainController : NancyModule
    {
        #region Explanation
        /*
         * 1. You have to put some Bultos on the Cinta
         * 2. The Brazo takes from the Cinta some Bultos
         * 3. The Brazo once it has a Bulto, puts it on the Prensa
         * 4. The Prensa, compress the Bultos.
         */
        #endregion
        #region Variables & Instances
        private static bool cintaIsStarted = false;
        private static bool brazoIsStarted = false;
        private static bool prensaIsStarted = false;

        private PilaBultos pilaBultos = new PilaBultos();
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

                return "ok";//mongo.connect() == true ? "POST 200 OK" : "POST 5xx ERROR";
            });
            #endregion

            #region Cinta
            //var rawRequest = Context.Request.Body.AsString();
            Get("/v1/cinta/state", x =>
            {;
                return cintaIsStarted == true ? "The Cinta is working":"The Cinta is off";
            }); // Tells if the Cinta is ON/OFF

            Get("/v1/cinta/sacar_bulto", x =>
            {;
                if (!cintaIsStarted)
                    return "First you have to turn ON the cinta";

                //Call to the sacarbultos service from the Brazo

                return "The bulto has been removed succesfully";
            });

            Get("/v1/cinta/poner_bulto", x =>
            {;
                //To put a bulto we use ZeroMQ to send the desired bulto to the Cinta.
                if (!cintaIsStarted)
                    return "First you have to turn ON the cinta";

                //ZMQ. Send request to the Server. When the server GETs the request, add the Bulto to the Cinta.

                //Remove the bulto from the Pila. We use FIFO for the Queue/Bultos list.
                var lst = new List<Bultos>();
                var blt = new Bultos_Manager();
                
                lst.RemoveAt(0);
                pilaBultos.pilaBultos = lst;

                return "The bulto is now on the Cinta";
            });

            Get("/v1/cinta/descontar_bulto", x =>
            {;


                return "?";
            });

            Get("/v1/cinta/turnon", x =>
            {
                ;
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

            #region Bultos
            Get("/v1/bultos/agregar", x =>
            {
                ;
                var bultos = new Bultos_Manager();
                var bulto = new Bultos();
                var lst = new List<Bultos>();

                pilaBultos.cantidadBultos = bultos.GetBultosQuantity();

                if (bultos.Insert(pilaBultos.cantidadBultos + 1))
                {
                    bulto.IDBulto = pilaBultos.cantidadBultos + 1;
                    lst.Add(bulto);
                }

                pilaBultos.pilaBultos = lst;

                Console.WriteLine("Now you have {0} pending bultos", pilaBultos.cantidadBultos + 1);

                return "The bulto has been added succesfully.";
            });

            Get("/v1/bultos/", x =>
            {
                ;
                if (!cintaIsStarted)
                    return "First you have to turn ON the cinta";

                //Call to the ponerBulto service

                return "?";
            });
            #endregion

            #region Brazo
            Get("/v1/brazo/tomar_bulto", x =>
            {;
                var bultos = new Bultos_Manager();
                var bulto = new Bultos();
                var lst = new List<Bultos>();

                pilaBultos.cantidadBultos = bultos.GetBultosQuantity();

                if (bultos.Insert(pilaBultos.cantidadBultos + 1))
                {
                    bulto.IDBulto = pilaBultos.cantidadBultos + 1;
                    lst.Add(bulto);
                }

                pilaBultos.pilaBultos = lst;

                Console.WriteLine("Now you have {0} pending bultos", pilaBultos.cantidadBultos + 1);

                return "The bulto has been added succesfully.";
            });

            #endregion
        }
    }
}