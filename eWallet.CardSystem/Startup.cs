using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eWallet.CardSystem.Startup))]
namespace eWallet.CardSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
