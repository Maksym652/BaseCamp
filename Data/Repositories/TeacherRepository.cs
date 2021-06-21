using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Models;
using WebApp.Core.Repositories;

namespace WebApp.Data.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, IRepository<Teacher>
    {
        private List<Teacher> teachers = new List<Teacher>();

        public override bool Create(Teacher teacher)
        {
            if (teachers.Find(t => t.Login == teacher.Login)==null && teachers.Find(t => t.Id == teacher.Id) == null)
            {
                teachers.Add(teacher);
                return true;
            }
            return false;
        }

        public override bool Delete(int id)
        {
            if(teachers.Find(t => t.Id == id) != null)
            {
                teachers.RemoveAll(t => t.Id == id);
                return true;
            }
            return false;
        }

        public override IEnumerable<Teacher> GetAll()
        {
            return teachers;
        }

        public override Teacher GetById(int id)
        {
            return teachers.Where(t => t.Id == id).FirstOrDefault();
        }

        public override bool Update(Teacher item)
        {
            Teacher t = teachers.Find(t => t.Id == item.Id);
            if (t != null)
            {
                teachers.Remove(t);
                teachers.Add(item);
            }
            return false;
        }
    }
}
