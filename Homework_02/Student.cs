using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2_Basecamp
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }
        public int Specialty { get; set; }
        public bool IsStudiedOnBudget { get; set; }
        
        public Student(int id, string name, int group, int specialty, bool isStudiedOnBudget)
        {
            this.Id = id;
            this.Name = name;
            this.Group = group;
            this.Specialty = specialty;
            this.IsStudiedOnBudget = isStudiedOnBudget;
        }
    }
}
