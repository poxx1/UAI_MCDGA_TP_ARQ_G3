using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Threading.Tasks;

namespace APIs
{
    public class Cinta_API
    {
        #region Variables & Instances
        public Cinta cinta = new Cinta();
        public Bultos bulto = new Bultos();
        #endregion

        public bool sacarBulto()
        {
            //Check if there is a Bulto on the cinta
            if (!cinta.HasPackage)
                return false; //There are 0 packages on the cinta.
            //Bultos counter --


            return true;
        }

        public bool ponerBulto()
        {   
            //As the Cinta is ON check first quantity of Bultos on it

            //If there is


            //Bultos counter ++


            return true;
        }
    }
}
