using Nancy;
using APIs;
using Nancy.Extensions;
using Newtonsoft.Json;
using Models;

namespace Server.Sensores
{
    public class PressController : NancyModule
    {
        #region Variables & Instances
        PressAPI pAPI = new PressAPI();
        #endregion
        public PressController()
        {
            #region Press
            Post("/v0/Press/Create", x =>
            {
                pAPI.Create();
                return "The Press has been created correctly";
            });

            Get("/v1/Press/TurnOn", x =>
            {
                return pAPI.TurnOn() == true ? true : false;
            });

            Get("/v1/Press/TurnOff", x =>
            {
                return pAPI.TurnOff() == true ? true : false;
            });

            Get("/v1/Press/State", x =>
            {   
                return pAPI.CurrentState();
            });
            #endregion
        }
    }
}