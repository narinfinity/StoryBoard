using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoryBoard.Web.Startup))]
namespace StoryBoard.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureWebApi(app);
        }
    }
}
