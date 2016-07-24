using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Paytime.Services;
using System.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

[assembly: OwinStartup(typeof(Paytime.Startup))]

namespace Paytime
{
    public class Startup
    {
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private static string authority = aadInstance + tenantId;
        private static int _cronHour =  Convert.ToInt32(ConfigurationManager.AppSettings["paytime:ReminderHour"].ToString());
        private static int _cronMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["paytime:ReminderMinute"].ToString());

        public void Configuration(IAppBuilder app)
        {
            var _eventNotification = new NotificationService();
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration.UseSqlServerStorage("PaytimeAzureDbContext");
            
            RecurringJob.AddOrUpdate(() => _eventNotification.GenerateNotification(), Cron.Daily(_cronHour, _cronMinutes));
            //RecurringJob.AddOrUpdate(() => _eventNotification.GenerateNotification(), Cron.Daily(09, 45));

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // Authentication
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority,
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        RoleClaimType = "roles"
                    }
                });
        }
    }
}
