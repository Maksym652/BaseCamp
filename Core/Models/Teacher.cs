using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Core.Models
{
    public class Teacher : User
    {
        public Teacher(int id, string name, string departmentName, string login, string password) : base(login, password, Enums.UserRole.TEACHER)
        {
            Id = id;
            Name = name;
            DepartmentName = departmentName;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Teacher t = (Teacher)obj;
                return t.Id == this.Id;
            }
        }
    }
}
