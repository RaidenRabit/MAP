using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MAPwClient.Controller
{
    public class UserController
    {
        private CallApiController apiController;

        public UserController()
        {
            apiController = new CallApiController();
        }

        public async Task<HttpResponseMessage> Login(string username, string password)
        {
            await apiController.GetToken(username, password);
            return null;
        }
    }
}
