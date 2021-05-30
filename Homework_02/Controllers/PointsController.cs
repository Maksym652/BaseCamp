namespace Homework_02.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Homework_02.Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller with CRUD operations for Point entities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private static IRepository<Point> pointRepository = new PointRepository();

        /// <summary>
        /// Returns all points.
        /// </summary>
        /// <returns>Collection that contains all points.</returns>
        [HttpGet]
        public IEnumerable<Point> GetAll()
        {
            return pointRepository.GetAll();
        }

        /// <summary>
        /// Returns point with specified ID.
        /// </summary>
        /// <param name="id">id of the point.</param>
        /// <returns>False if student id is less than 0 or if the list already contains student with the same Id, otherwise true.</returns>
        [HttpGet]
        [Route("{id}")]
        public Point GetById([FromRoute] int id)
        {
            return pointRepository.GetById(id);
        }

        /// <summary>
        /// Creates new point and adds it to the list.
        /// </summary>
        /// <param name="id">Point id.</param>
        /// <param name="studentId">ID of the student, who has got a point.</param>
        /// <param name="mark">Point on a 100-point scale.</param>
        /// <param name="subjectName">Name of the subject.</param>
        /// <param name="date">Date, when student has got a point.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPost]
        public IActionResult Create([FromQuery] int id, [FromQuery] int studentId, [FromQuery] float mark, [FromQuery] string subjectName, [FromQuery] DateTime date)
        {
            if (pointRepository.Create(new Point(id, mark, studentId, subjectName, date)))
            {
                return this.Ok("Operation successful");
            }
            else
            {
                return this.Ok("Operation unsuccessful");
            }
        }

        /// <summary>
        /// Removes point with specified id from the list.
        /// </summary>
        /// <param name="id">Point id.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpDelete]
        public IActionResult Delete([FromBody] int id)
        {
            if (pointRepository.Delete(id))
            {
                return this.Ok("Operation successful");
            }
            else
            {
                return this.Ok("Operation unsuccessful");
            }
        }

        /// <summary>
        /// Updates point with specified ID.
        /// </summary>
        /// <param name="id">Point id.</param>
        /// <param name="studentId">ID of the student, who has got a point.</param>
        /// <param name="mark">Point on a 100-point scale.</param>
        /// <param name="subjectName">Name of the subject.</param>
        /// <param name="date">Date, when student has got a point.</param>
         /// <returns>Object that represents result of the method work.</returns>
        [HttpPut]
        public IActionResult Update([FromQuery] int id, [FromQuery] int studentId, [FromQuery] float mark, [FromQuery] string subjectName, [FromQuery] DateTime date)
        {
            if (pointRepository.Update(new Point(id, mark, studentId, subjectName, date)))
            {
                return this.Ok("Operation successful");
            }
            else
            {
                return this.Ok("Operation unsuccessful");
            }
        }
    }
}
