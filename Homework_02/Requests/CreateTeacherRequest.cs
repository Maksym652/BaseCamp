using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Api.Requests
{
    public class CreateTeacherRequest
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public CreateTeacherRequest() { }

        public CreateTeacherRequest(string login, string password, string name, string departmentName)
        {
            this.Login = login;
            this.Password = password;
            this.Name = name;
            this.DepartmentName = departmentName;
        }
    }
}
