using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using Models;
using APIs;

namespace Server.Sensores.Controller
{
    public class SensorsController : NancyModule
    {
        SensorsAPI apiS = new SensorsAPI();
        PressAPI apiP = new PressAPI();
        public SensorsController()
        {
            Get("/v1/Press/Compress", _ =>
            {
                //Change the state of the arm
                if (!apiS.CheckActive() && apiP.CurrentState())
                {
                    apiS.ArmDOWN();
                    apiS.OcupatePress();

                    return true;
                }

                else
                {
                    return false;
                }

            });

            Get("/v1/Press/Release", _ =>
            {
                //Releases the bultos and changes the state of the ARM
                //Change the state of the arm
                if (apiS.CheckActive() && apiP.CurrentState())
                {
                    apiS.ArmUP();
                    apiS.RelasePress();

                    return true;
                }

                else
                {
                    return false;
                }
            });
        }
    }
}