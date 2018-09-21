using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace MAPeApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
    }

    public enum FindUserBy
    {
        userID = 1,
        username = 2,
        email = 3,
        nickname = 4
    }

}