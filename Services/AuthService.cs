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
        private readonly IUserRepository _repository;

        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool DeleteAccount(string username)
        {
            return _repository.DeleteUser(username);
        }


        public bool ChangePassword(string username, string newPassword)
        {
            return _repository.UpdatePassword(username, newPassword);
        }

        public bool Signup(string username, string password)
        {
            if (_repository.GetUser(username) != null)
                return false;

            var user = new User { Username = username, Password = password };
            _repository.AddUser(user);
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = _repository.GetUser(username);
            return user != null && user.Password == password;
        }
    }
}