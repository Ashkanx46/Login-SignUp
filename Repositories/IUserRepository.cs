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

        bool DeleteUser(string username);
        bool UpdatePassword(string username, string newPassword); // 👈 اینو اضافه کن
        void AddUser(User user);          // اضافه کردن کاربر جدید
        User? GetUser(string username);   // گرفتن کاربر بر اساس یوزرنیم
        List<User> GetAllUsers();         // برگردوندن همه کاربرها
    }
}