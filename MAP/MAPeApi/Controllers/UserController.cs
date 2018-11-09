using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using MAPeApi.DataManagement;
using MAPeApi.DataManagement.iDM;
using MAPeApi.Models;

namespace MAPeApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserDM _userDm;

        public UserController()
        {
            _userDm = new UsersDM();
        }

        [Authorize]
        [HttpGet]
        [Route("api/user/GetUser")]
        public HttpResponseMessage GetUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            string userId = identity.Claims.Where(c => c.Type.ToString() == "userId").Select(c => c.Value).ToList().FirstOrDefault();

            User user = _userDm.GetUser(userId, FindUserBy.userID);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }
}
