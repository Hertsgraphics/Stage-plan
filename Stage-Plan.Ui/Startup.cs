using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stage_plan.Ui.Startup))]
namespace Stage_plan.Ui
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
