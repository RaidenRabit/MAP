using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MAPwClient.WebApiCommunication;

namespace MAPwClient.Controller
{
    public class LoginController
    {
        private HttpClient conn;
        public LoginController()
        {
            conn = new WebApiConnection().GetClient();
        }

        public async Task GetUser(string username, string password)
        {
            request = new HttpRequestMessage(HttpMethod.Get, $"api/documents/{username}?password={password}");
            conn.ReadAsync(request);

        }
    }
}
