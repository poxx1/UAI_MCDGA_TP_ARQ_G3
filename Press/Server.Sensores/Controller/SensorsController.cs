using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using Models;
using APIs;
using Observer;
using System.Collections.Generic;

namespace Server.Sensores.Controller
{
    public class SensorsController : NancyModule, ISubject
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
                    //apiS.ArmUP();
                    Notify();
                    apiS.RelasePress();

                    return true;
                }

                else
                {
                    return false;
                }
            }); 
        }

        #region Subject
        public bool State { get; set; } = false;

        private List<IObserver> ListObservers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            ListObservers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            ListObservers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in ListObservers)
            {
                observer.Update(this);
            }
        }

        #endregion
    }
}