using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    public class OutputModel
    {
        public float CantidadEstrellas { get; set; }
        public float CantidadPlanetas { get; set; }
        public float CantidadSatelites { get; set; }

        public float avgDistanceEstrellas { get; set; }
        public float avgDistancePlanetas { get; set; }
        public float avgDistanceSatelites { get; set; }

        public float avgTamEstrellas { get;set; }
        public float avgTamPlanetas { get; set; }
        public float avgTamSatelites { get; set; }

        public float avgTemperatureEstrellas { get; set; }
    }
}
