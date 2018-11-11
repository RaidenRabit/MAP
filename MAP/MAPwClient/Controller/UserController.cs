using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MAPeApi.Models;
using Newtonsoft.Json;

namespace MAPwClient.Controller
{
    public class UserController
    {
        private CallApiController apiController;

        public UserController()
        {
            apiController = new CallApiController();
        }

        public async Task<User> Login(string username, string password)
        {
            if(await apiController.GetToken(username, password))
             {
                var response = await apiController.GetFromApi("api/user/GetUser");
                return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());

            }
            else
            {
                return null;
            }
        }
    }
}
