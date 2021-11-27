using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Machines
    {
        public bool IsOn { get; set; }
        public bool HasPackage { get; set; }

        //Turning on the desired machine
        public bool turnOn()
        {
            return true;
        }

        //Turning off the desired machine
        public bool turnOff()
        {
            return true;
        }
    }
}
