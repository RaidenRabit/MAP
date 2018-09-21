using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAPeApi.DataAccess;
using MAPeApi.Models;

namespace MAPeApi.DataManagement
{
    public class UsersDM
    {
        private DbUser dbUser;

        public UsersDM()
        {
            dbUser = new DbUser();
        }

        public User Login(string username, string password)
        {
            string userID = dbUser.Authenticate(username, password).ToString();
            User user = GetUser(userID, FindUserBy.userID);

            return user;
        }

        public User GetUser(string userID, FindUserBy by)
        {
            return dbUser.GetUser(userID, by);
        }
    }
}