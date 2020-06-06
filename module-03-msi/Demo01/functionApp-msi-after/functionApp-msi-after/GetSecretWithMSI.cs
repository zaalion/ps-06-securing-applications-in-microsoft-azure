using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;
using System;

namespace functionApp_msi_after
{
    public static class GetSecretWithMSI
    {
        [FunctionName("GetSecretWithMSI")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessToken));

                // Get the secret

                var secretUrl = "https://kv-msi-01.vault.azure.net/secrets/myname/56c2905096f14c689d928da072139c72";
                var secret = kv.GetSecretAsync(secretUrl).Result;

                var myName = secret.Value;

                return myName != null
                    ? (ActionResult)new OkObjectResult($"Hello, {myName}")
                    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }
            catch(Exception ex)
            {
                return (ActionResult)new OkObjectResult(ex.Message);
            }
        }

        private static async Task<string> GetAccessToken(string authority, string resource, string scope)
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            string accessToken = await azureServiceTokenProvider.GetAccessTokenAsync("https://vault.azure.net");

            return accessToken;

            //var clientId = "1389cdbd-8c73-42df-80f9-0cda965863ed";
            //var clientSecret = "9gF8UK8m.ZCGlsweo3ls@YF.lxk@/S2J";

            //var authenticationContext = new AuthenticationContext(authority);

            //var cCreds = new ClientCredential(clientId, clientSecret);

            //AuthenticationResult result = await authenticationContext.AcquireTokenAsync(resource, cCreds);

            //return result.AccessToken;
        }
    }
}
