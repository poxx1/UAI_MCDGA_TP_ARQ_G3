using Owin;

namespace Server.Nancy
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
