using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stage_Plan.Ui.Startup))]
namespace Stage_Plan.Ui
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
