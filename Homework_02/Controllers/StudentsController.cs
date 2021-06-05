namespace Basecamp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using WebApp.Api.Requests;
    using WebApp.Api.Responses;
    using WebApp.Core.Models;
    using WebApp.Core.Repositories;
    using WebApp.Data.Repositories;

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
        public List<StudentResponse> Get()
        {
            return (List<StudentResponse>)studentRepository.GetAll().Select(st => new StudentResponse(st));
        }

        /// <summary>
        /// Returns student with specified ID.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>Student with specified ID.</returns>
        [HttpGet]
        [Route("{id}")]
        public StudentResponse Get([FromRoute] int id)
        {
            return new StudentResponse(studentRepository.GetById(id));
        }

        /// <summary>
        /// Creates new student and adds it to the list.
        /// </summary>
        /// <param name="studentRequest">Object of CreateStudentRequest class.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] CreateStudentRequest studentRequest)
        {
            int id = 20000000 + ((DateTime.Now.Year - 2000) * 100000) + (studentRequest.Group * 100) + pointRepository.GetAll().Where(p => studentRepository.GetById(p.StudentId).Group == studentRequest.Group).Count();
            if (studentRepository.Create(new Student(id, studentRequest.Name, studentRequest.Group, studentRequest.Specialty, studentRequest.IsStudiedOnBudget)))
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
        /// <param name="request">Update student request.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateStudentRequest request)
        {
            if (studentRepository.Update(new Student(id, studentRepository.GetById(id).Name, request.Group, studentRepository.GetById(id).Specialty, request.IsStudiedOnBudget)))
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