using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Api.Requests
{
    public class UpdateTeacherRequest
    {
        public string Password { get; set; }

        public string DepartmentName { get; set; }

        public UpdateTeacherRequest() { }

        public UpdateTeacherRequest(string password, string departmentName)
        {
            this.Password = password;
            this.DepartmentName = departmentName;
        }
    }
}
