using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MAPwClient.Model;
using MAPwClient.WebApiCommunication;
using Newtonsoft.Json;

namespace MAPwClient.Controller
{
    public class CallApiController
    {
        private Token token;
        
        public async Task<bool> GetToken(string userName, string password)
        {
            WebApiConnection con = new WebApiConnection();
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>( "grant_type", "password" ),
                new KeyValuePair<string, string>( "username", userName ),
                new KeyValuePair<string, string> ( "Password", password )
            };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = con.GetClient())
            {
                var response = await client.PostAsync("/token", content);
                if (response.IsSuccessStatusCode)
                {
                    token = JsonConvert.DeserializeObject<Token>( await response.Content.ReadAsStringAsync() );
                    return true;
                }
            }
            return false;
        }
        public async Task<HttpResponseMessage> GetFromApi(string request)
        {
            WebApiConnection con = new WebApiConnection();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = con.GetClient())
            {
                if (!token.access_token.Equals(""))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.access_token);
                }
                else
                {
                    throw new Exception("Token expired, please login again");
                }
                var response = await client.GetAsync(request);
                return response;
            }
        }
    }
}
