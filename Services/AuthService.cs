using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using login.Models;
using login.Repositories;

namespace login.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Signup(string username, string password)
        {
            var existingUser = _userRepository.GetUser(username);
            if (existingUser != null) return false;

            var user = new User
            {
                Username = username,
                Password = password
            };

            _userRepository.AddUser(user);
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.GetUser(username);
            return user != null && user.Password == password;
        }

        public bool ChangePassword(string username, string newPassword)
        {
            return _userRepository.UpdatePassword(username, newPassword);
        }

        public bool DeleteAccount(string username)  // ✅ متد حذف اکانت
        {
            return _userRepository.DeleteUser(username);
        }
    }
}