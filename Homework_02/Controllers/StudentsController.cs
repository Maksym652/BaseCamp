namespace Basecamp
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using WebApp.Api;
    using WebApp.Api.Requests;
    using WebApp.Api.Responses;
    using WebApp.Api.Validators;
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
        private static IRepository<Student> studentRepository;
        private static IRepository<Point> pointRepository;

        public StudentsController(IRepository<Student> studentRepo, IRepository<Point> pointRepo)
        {
            studentRepository = studentRepo;
            pointRepository = pointRepo;
        }

        /// <summary>
        /// Returns list with all students.
        /// </summary>
        /// <returns> .</returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetAllStudents()
        {
            return this.Ok(studentRepository.GetAll().Select(st => new StudentResponse(st)));
        }

        /// <summary>
        /// Returns student with specified ID.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>Student with specified ID.</returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public IActionResult GetStudentById([FromRoute] int id)
        {
            if (studentRepository.GetById(id) != null)
            {
                return this.Ok(new StudentResponse(studentRepository.GetById(id)));
            }
            else
            {
                return this.NotFound("Student with specified ID not fount.");
            }
        }

        /// <summary>
        /// Creates new student and adds it to the list.
        /// </summary>
        /// <param name="studentRequest">Object of CreateStudentRequest class.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult AddStudent([FromBody] CreateStudentRequest studentRequest)
        {
            int id = 20000000 + ((DateTime.Now.Year - 2000) * 100000) + (studentRequest.Group * 100) + studentRepository.GetAll().Where(st => st.Group == studentRequest.Group).Count() + 1;
            Student st = new Student(studentRequest.Login, studentRequest.Password, id, studentRequest.Name, studentRequest.Group, studentRequest.Specialty, studentRequest.IsStudiedOnBudget);
            if (studentRepository.Create(st))
            {
                return this.Ok(st);
            }
            else
            {
                return this.BadRequest(st);
            }
        }

        /// <summary>
        /// Updates student with specified ID.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <param name="request">Update student request.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        [Route("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateStudentRequest request)
        {
            Student st = new Student(studentRepository.GetById(id).Login, request.NewPassword, id, studentRepository.GetById(id).Name, request.Group, studentRepository.GetById(id).Specialty, request.IsStudiedOnBudget);
            if (studentRepository.Update(st))
            {
                return this.Ok(st);
            }
            else
            {
                return this.BadRequest("Operation unsuccessful");
            }
        }

        /// <summary>
        /// Removes student with specified ID.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete([FromRoute] int id)
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
        [Authorize(Roles = "STUDENT, TEACHER")]
        public IActionResult GetPoints([FromRoute] int id)
        {
            return this.Ok(pointRepository.GetAll().Where(p => p.StudentId == id));
        }


        [HttpPost("/token")]
        public IActionResult Token(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
            };

            return new JsonResult(response);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            Student student = studentRepository.GetAll().Where(x => x.Login == username && x.Password == password).FirstOrDefault();
            if (student != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, student.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, student.Role.ToString()),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}