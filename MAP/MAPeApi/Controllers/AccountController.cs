using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MAPeApi.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        [Authorize]
        [Route("CreateAccount")]
        public IHttpActionResult CreateAccount()
        {
            return null;
        }
    }
}
