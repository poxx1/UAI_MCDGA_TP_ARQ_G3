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
                return pAPI.TurnOn() == true ? "Press is now turned ON" : "The press is already ON";
            });

            Get("/v1/Press/TurnOff", x =>
            {
                return pAPI.TurnOff() == true ? "Press is now turned OFF":"Can't turn off the press, since it's already off";
            });

            Get("/v1/Press/State", x =>
            {   
                return pAPI.CurrentState();
            });
            #endregion
        }
    }
}