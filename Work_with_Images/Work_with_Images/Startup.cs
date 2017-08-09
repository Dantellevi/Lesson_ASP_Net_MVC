using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Work_with_Images.Startup))]
namespace Work_with_Images
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
