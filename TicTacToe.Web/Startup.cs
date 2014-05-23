using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TicTacToe.Web.Startup))]
namespace TicTacToe.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}