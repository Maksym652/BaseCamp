namespace Homework2_Basecamp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Homework_02;
    using Homework_02.Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller with CRUD operations for Student entities.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private static IRepository<Student> studentRepository = new StudentRepository();
        private static IRepository<Point> pointRepository = new PointRepository();

        /// <summary>
        /// Returns list with all students.
        /// </summary>
        /// <returns>List, that contains all students.</returns>
        [HttpGet]
        public List<Student> Get()
        {
            return (List<Student>)studentRepository.GetAll();
        }

        /// <summary>
        /// Returns student with specified ID.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>Student with specified ID.</returns>
        [HttpGet]
        [Route("{id}")]
        public Student Get([FromRoute] int id)
        {
            return studentRepository.GetById(id);
        }

        /// <summary>
        /// Creates new student and adds it to the list.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <param name="name">Student name.</param>
        /// <param name="group">Student group.</param>
        /// <param name="specialty">Student specialty.</param>
        /// <param name="isStudiedOnBudget">Bool value showing if student is studying on budget (for free).</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPost]
        public IActionResult Post([FromQuery] int id, [FromQuery] string name, [FromQuery] int group, [FromQuery] int specialty, [FromQuery] bool isStudiedOnBudget)
        {
            if (studentRepository.Create(new Student(id, name, group, specialty, isStudiedOnBudget)))
            {
                return this.Ok("Operation successful");
            }
            else
            {
                return this.Ok("Operation unsuccessful");
            }
        }

        /// <summary>
        /// Updates student with specified ID.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <param name="name">Student name.</param>
        /// <param name="group">Student group.</param>
        /// <param name="specialty">Student specialty.</param>
        /// <param name="isStudiedOnBudget">Bool value showing if student is studying on budget (for free).</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPut]
        public IActionResult Put([FromQuery] int id, [FromQuery] string name, [FromQuery] int group, [FromQuery] int specialty, [FromQuery] bool isStudiedOnBudget)
        {
            if (studentRepository.Update(new Student(id, name, group, specialty, isStudiedOnBudget)))
            {
                return this.Ok("Operation successful");
            }
            else
            {
                return this.Ok("Operation unsuccessful");
            }
        }

        /// <summary>
        /// Removes student with specified ID.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpDelete]
        public IActionResult Delete([FromBody] int id)
        {
            if (studentRepository.Delete(id))
            {
                return this.Ok("Operation successful");
            }
            else
            {
                return this.Ok("Operation unsuccessful");
            }
        }

        /// <summary>
        /// Returns a collection of points of the student with specified ID.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>Collection of the points of this student</returns>
        [HttpGet]
        [Route("{id}/points")]
        public IEnumerable<Point> GetPoints([FromRoute] int id)
        {
            return pointRepository.GetAll().Where(p => p.StudentId == id);
        }
    }
}