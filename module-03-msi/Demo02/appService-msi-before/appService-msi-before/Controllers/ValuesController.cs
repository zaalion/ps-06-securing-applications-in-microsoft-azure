using Microsoft.Azure.Services.AppAuthentication;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace appService_msi_before.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public Dictionary<string, string> Get()
        {
            var connectingString = "Server=tcp:azuresqlmsidemosrv.database.windows.net,1433;" +
                "Initial Catalog=MSIDEMO;Persist Security Info=False" +
                ";MultipleActiveResultSets=False;" +
                "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var capitals = new Dictionary<string, string>();

            using (var sqlConnection = new SqlConnection(connectingString))
            {
                var sqlCommand = new SqlCommand("SELECT Country, Capital FROM CountryInfo", sqlConnection);

                var accessToken = (new AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/").Result;
                sqlConnection.AccessToken = accessToken;

                sqlConnection.Open();

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    capitals.Add(reader["Country"].ToString(), reader["Capital"].ToString());
                }

                sqlConnection.Close();
            }

            return capitals;
        }
    }
}