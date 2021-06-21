using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Api.Responses
{
    public class TeacherResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public TeacherResponse(Teacher teacher)
        {
            Id = teacher.Id;
            Name = teacher.Name;
            Department = teacher.DepartmentName;
        }
    }
}
