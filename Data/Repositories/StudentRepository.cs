namespace WebApp.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApp.Core.Models;
    using WebApp.Core.Repositories;

    /// <summary>
    /// Repository for interraction with Student entities.
    /// </summary>
    public class StudentRepository : BaseRepository<Student>, IRepository<Student>
    {
        private static List<Student> students = new List<Student>();

        /// <summary>
        /// Creates new Student entity and adds it to the list.
        /// </summary>
        /// <param name="student">Student entity.</param>
        /// <returns>False if student id is less than 0 or if the list already contains student with the same Id, otherwise true.</returns>
        public override bool Create(Student student)
        {
            if (student.Id < 0 || students.Find(st => st.Id == student.Id) != null)
            {
                return false;
            }

            students.Add(student);
            return true;
        }

        /// <summary>
        /// Removes student with specified id from the list.
        /// </summary>
        /// <param name="id">ID of the student to be deleted.</param>
        /// <returns>False if the list is not contain student with specified ID, otherwise true.</returns>
        public override bool Delete(int id)
        {
            if (students.Find(st => st.Id == id) == null)
            {
                return false;
            }

            students.Remove(students.Find(st => st.Id == id));
            return true;
        }

        /// <summary>
        /// Returns the list of all students.
        /// </summary>
        /// <returns>Collection that contains all Student entities.</returns>
        public override IEnumerable<Student> GetAll()
        {
            return students;
        }

        /// <summary>
        /// Returns student with specified id.
        /// </summary>
        /// <param name="id">ID of the student to be returned.</param>
        /// <returns>Student entity with specified ID.</returns>
        public override Student GetById(int id)
        {
            return students.Find(st => st.Id == id);
        }

        /// <summary>
        /// Removes student that has the same ID as item, and adds item to the list.
        /// </summary>
        /// <param name="item">Item that replace student with same ID.</param>
        /// <returns>False if list not contains student with the same ID as item, otherwise true.</returns>
        public override bool Update(Student item)
        {
            Student student = students.Find(st => st.Id == item.Id);
            if (student == null)
            {
                return false;
            }

            students.Remove(student);
            students.Add(item);
            return true;
        }
    }
}
