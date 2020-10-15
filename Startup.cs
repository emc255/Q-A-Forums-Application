using CourseProject_StackOverFlowV2.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CourseProject_StackOverFlowV2.Startup))]
namespace CourseProject_StackOverFlowV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
     
    }
    
}
