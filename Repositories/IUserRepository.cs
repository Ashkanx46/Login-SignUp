using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using login.Models;

namespace login.Repositories
{


    public interface IUserRepository
    {

        User? GetUser(string username);
        void AddUser(User user);
        List<User> GetAllUsers();
        bool UpdatePassword(string username, string newPassword);
        bool DeleteUser(string username);
    }
}