using Owin;

namespace Server.Sensores
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            //Archivo de configuracion
            app.UseNancy();
        }
    }
}
