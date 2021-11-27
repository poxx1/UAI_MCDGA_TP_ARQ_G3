using Owin;

namespace APItest.Nancy
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
