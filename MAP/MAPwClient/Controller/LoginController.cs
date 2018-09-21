using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MAPwClient.Controller
{
    public class LoginController
    {
        private CallApiController apiController;

        public LoginController()
        {
            apiController = new CallApiController();
        }

        public async Task<HttpResponseMessage> Login(string username, string password)
        {
            if (await apiController.GetToken(username, password))
                return await apiController.GetFromApi("/api/data/Admin");
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
