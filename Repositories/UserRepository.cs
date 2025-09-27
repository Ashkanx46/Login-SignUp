using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using login.Models;

namespace login.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public User GetUser(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            var user = GetUser(username);
            if (user != null)
            {
                user.Password = newPassword;
                return true;
            }
            return false;
        }
    }
}