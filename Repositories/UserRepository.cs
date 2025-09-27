using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using login.Models;
using login.Data;

namespace login.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;


        public bool DeleteUser(string username)
        {
            var user = GetUser(username);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }


        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }


        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            var user = GetUser(username);
            if (user != null)
            {
                user.Password = newPassword;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}