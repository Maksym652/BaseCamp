using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2_Basecamp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        static List<Student> students = new List<Student>()
        {
            new Student(1, "Ivanenko", 201, 122, true),
            new Student(2, "Petrenko", 202, 122, false),
            new Student(3, "Vasylenko", 205, 123, true),
            new Student(4, "Stepanenko", 208, 121, false),
            new Student(5, "Kovalenko", 209, 121, true),
        };

        [HttpGet]
        public List<Student> Get()
        {
            return students;
        }

        [HttpGet]
        [Route("{id}")]
        public Student Get([FromRoute] int id)
        {
            return students.Find(student => student.Id == id);
        }

        [HttpPost]
        public void Post([FromQuery] int id, [FromQuery] string name, [FromQuery] int group, [FromQuery] int specialty, [FromQuery] bool isStudiedOnBudget)
        {
            students.Add(new Student(id, name, group, specialty, isStudiedOnBudget));
        }

        [HttpPut]
        public void Put([FromQuery] int id, [FromQuery] string name, [FromQuery] int group, [FromQuery] int specialty, [FromQuery] bool isStudiedOnBudget)
        {
            Student st = students.Find(student => student.Id == id);
            students.Remove(st);
            st.Name = name;
            st.Group = group;
            st.Specialty = specialty;
            st.IsStudiedOnBudget = isStudiedOnBudget;
            students.Add(st);
        }

        [HttpDelete]
        public void Delete([FromBody] int id)
        {
            students.Remove(students.Find(st => st.Id == id));
        }
    }
}