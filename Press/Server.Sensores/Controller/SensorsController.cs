using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using Models;
using APIs;

namespace Server.Sensores.Controller
{
    public class SensorsController : NancyModule
    {
        public SensorsController()
        {
            Get("/v1/status", _ =>
            {
                return "GET 200: OK";
            });
        }
    }
}
