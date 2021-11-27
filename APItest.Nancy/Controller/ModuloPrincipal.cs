using Nancy;
using Nancy.Extensions;
using Properties;
using Methods;

namespace APItest.Nancy.Controller
{
    public class ModuloPrincipal : NancyModule
    {
        //Comprobacion del servicio
        private static bool isStarted = false;
        public ModuloPrincipal()
        {
            #region Tests + Status 

            Get("/v1/status", _ =>
            {
                return "GET 200: OK";
            });

            Get("/v1/test", _ =>
            {
                return "GET Test Reponse";
            });

            Post("/v1/test", _ =>
            {
                return "POST Test Reponse";
            });

            #endregion

            #region Calculator

            Post("/v1/Info", x =>
            {
                //Objeto a modificar con el POST
                var product = new CuerpoCeleste();
                //Obtengo el raw
                var rawRequest = Context.Request.Body.AsString();
                //Lo paso a la clase

                var instancia = new Randomizer();

                if (isStarted)
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

            Get("/v1/Init", x =>
            {
                if (isStarted)
                    return "El servicio ya esta inicializado";

                isStarted = true;

                return "Se inicializo el servicio";

            });

            Get("/v1/Finish", x =>
            {
                if (isStarted)
                {
                    isStarted = false;

                    return "Se finalizo el servicio";
                }

                return "Primero tiene que iniciar un servicio";
            });
            #endregion


        }
    }
}
