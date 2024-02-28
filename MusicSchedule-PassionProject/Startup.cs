using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicSchedule_PassionProject.Startup))]
namespace MusicSchedule_PassionProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
