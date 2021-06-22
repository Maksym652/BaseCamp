namespace BaseCamp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using WebApp.Api.Requests;
    using WebApp.Api.Responses;
    using WebApp.Core.Models;
    using WebApp.Core.Repositories;
    using WebApp.Data.Repositories;

    /// <summary>
    /// Controller with CRUD operations for Point entities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private static IRepository<Point> pointRepository;

        public PointsController(IRepository<Point> pointRepo)
        {
            pointRepository = pointRepo;
        }

        /// <summary>
        /// Returns all points.
        /// </summary>
        /// <returns>Collection that contains all points.</returns>
        [HttpGet]
        [Authorize(Roles = "STUDENT, TEACHER")]
        public IActionResult GetAllPoints()
        {
            return Ok(pointRepository.GetAll()
                .Select(p => new PointResponse(p)));
        }

        /// <summary>
        /// Returns point with specified ID.
        /// </summary>
        /// <param name="id">id of the point.</param>
        /// <returns>False if student id is less than 0 or if the list already contains student with the same Id, otherwise true.</returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "STUDENT, TEACHER")]
        public IActionResult GetPointById([FromRoute] int id)
        {
            Point p = pointRepository.GetById(id);
            if (p == null)
            {
                return NotFound("Point with specified ID not found.");
            }

            return Ok(new PointResponse(p));
        }

        /// <summary>
        /// Creates new point and adds it to the list.
        /// </summary>
        /// <param name="request">Point create request.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPost]
        [Authorize(Roles = "TEACHER")]
        public IActionResult AddPoint([FromBody] CreatePointRequest request)
        {
            int id = ((request.StudentId - 20000000) * 100) + pointRepository.GetAll()
                .Where(p => p.StudentId == request.StudentId && p.Date.DayOfYear == DateTime.Now.DayOfYear && p.Date.Year == DateTime.Now.Year).Count() + 1;
            Point point = new Point(id, request.Mark, request.StudentId, request.Subject, DateTime.Now, request.Task);
            if (pointRepository.Create(point))
            {
                return Ok(point);
            }
            else
            {
                return BadRequest("Operation unsuccessful");
            }
        }

        /// <summary>
        /// Removes point with specified id from the list.
        /// </summary>
        /// <param name="id">Point id.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "TEACHER")]
        public IActionResult DeletePoint([FromRoute] int id)
        {
            if (pointRepository.Delete(id))
            {
                return Ok("Point with specified ID deleted successfully.");
            }
            else
            {
                return NotFound("Point with specified ID not found.");
            }
        }

        /// <summary>
        /// Updates point with specified ID.
        /// </summary>
        /// <param name="id">Point id.</param>
        /// <param name="request">Point update request.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "TEACHER")]
        public IActionResult UpdatePoint([FromRoute] int id, [FromBody] UpdatePointRequest request)
        {
            Point pointBeforeUpdating = pointRepository.GetById(id);
            if (pointBeforeUpdating == null)
            {
                return NotFound("Point with specified ID not found.");
            }

            Point updatedPoint = new Point(id, request.Mark, pointBeforeUpdating.StudentId, pointBeforeUpdating.Subject, DateTime.Now, request.Task);
            if (pointRepository.Update(updatedPoint))
            {
                return Ok(updatedPoint);
            }
            else
            {
                return NotFound("Poind with specified ID not found.");
            }
        }
    }
}
