using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAPwClient.WebApiCommunication
{
    class WebApiConnection
    {
        private HttpClient client;
        public WebApiConnection()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:60792");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async void GetUser(string username, string password)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("/api/student/" + username + password);
                response.EnsureSuccessStatusCode(); // Throw on error code. 
                var students = await response.Content.ReadAsAsync<DynamicAttribute>();
            }
            catch (Exception)
            {

            }
        }
    }
}
