using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAPeApi.Models;

namespace MAPeApi.DataManagement.iDM
{
    public interface IUserDM
    {
        User Login(string username, string password);

        User GetUser(string userId, FindUserBy by);
    }
}
