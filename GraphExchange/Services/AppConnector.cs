using Microsoft.Identity.Client;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace GraphExchange.Services
{
    public class AppConnector
    {
        public async Task<AuthenticationResult> AcquireToken()
        {
            AuthenticationConfig config = new AuthenticationConfig()
            {
                ClientId = ConfigurationManager.AppSettings["ida:ClientId"],
                Tenant = ConfigurationManager.AppSettings["ida:TenantId"],
                ClientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"],
                Instance = ConfigurationManager.AppSettings["ida:Instance"],
                ApiUrl = ConfigurationManager.AppSettings["ida:ApiUrl"]

            };
            string[] scopes = new string[] { $"{config.ApiUrl}.default" };
            AuthenticationResult result = null;
            IConfidentialClientApplication app;

            if (!String.IsNullOrWhiteSpace(config.ClientSecret))
            {
                app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                    .WithClientSecret(config.ClientSecret)
                    .WithAuthority(new Uri(config.Authority))
                    .Build();
            }
            else
            {
                throw new Exception("Invalid client secret");
            }
            
            try
            {
                result = await app.AcquireTokenForClient(scopes)
                    .ExecuteAsync();
            }
            catch (Exception ex) when (ex.Message.Contains("AADSTS70011"))
            {

                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}