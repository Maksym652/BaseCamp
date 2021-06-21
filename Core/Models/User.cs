using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Enums;

namespace WebApp.Core.Models
{
    public abstract class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public User(string login, string password, UserRole role)
        {
            this.Login = login;
            this.Password = password;
            this.Role = role;
        }
    }
}
