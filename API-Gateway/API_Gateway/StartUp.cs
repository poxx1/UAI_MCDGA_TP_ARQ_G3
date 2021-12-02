using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway
{
    public class StartUp
    {
        public void Configuration(IAppBuilder cfg)
        {
            cfg.UseNancy();
        }
    }
}