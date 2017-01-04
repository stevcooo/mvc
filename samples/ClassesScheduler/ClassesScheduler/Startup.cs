using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClassesScheduler.Startup))]
namespace ClassesScheduler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
