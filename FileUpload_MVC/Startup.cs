using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FileUpload_MVC.Startup))]
namespace FileUpload_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
