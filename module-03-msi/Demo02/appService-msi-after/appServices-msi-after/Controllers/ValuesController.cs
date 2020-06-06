using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace appServices_msi_after.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public Dictionary<string, string> Get()
        {
            //var connectingString = "Server=tcp:azuresqlmsidemosrv.database.windows.net,1433;Initial Catalog=MSIDEMO;Persist Security Info=False" +
            //    ";User ID=rezaadmin;Password=MySecurePass!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var connectingString = "Server=tcp:azuresqlmsidemosrv.database.windows.net,1433;Initial Catalog=MSIDEMO;Persist Security Info=False" +
                ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var capitals = new Dictionary<string, string>();

            try
            {
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
            }
            catch(Exception ex)
            {
                capitals.Add("error", ex.Message);
            }

            

            return capitals;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
