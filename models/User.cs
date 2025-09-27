using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login.Models
{
    public class User
    {
        [Key]  // این مشخص می‌کنه که Id کلید اصلیه
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;   // نام کاربری
        [Required]
        public string Password { get; set; } = string.Empty;   // رمز عبور
    }
}
