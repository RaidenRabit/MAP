using MAPeApi.DataAccess;
using MAPeApi.DataManagement.iDM;
using MAPeApi.Models;

namespace MAPeApi.DataManagement
{
    public class UsersDM : IUserDM
    {
        private readonly DbUser _dbUser;

        public UsersDM()
        {
            _dbUser = new DbUser();
        }

        public User Login(string username, string password)
        {
            string userID = _dbUser.Authenticate(username, password).ToString();
            User user = GetUser(userID, FindUserBy.userID);

            return user;
        }

        public User GetUser(string userId, FindUserBy by)
        {
            return _dbUser.GetUser(userId, by);
        }
    }
}