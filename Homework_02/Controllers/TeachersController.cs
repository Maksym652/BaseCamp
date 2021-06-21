using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Api.Requests;
using WebApp.Api.Responses;
using WebApp.Core.Models;
using WebApp.Core.Repositories;
using WebApp.Data.Repositories;

namespace WebApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        IRepository<Teacher> teacherRepository;

        public TeachersController(IRepository<Teacher> teacherRepo)
        {
            teacherRepository = teacherRepo;
        }

        /// <summary>
        /// Returns list with all teachers.
        /// </summary>
        /// <returns> .</returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            return this.Ok(teacherRepository.GetAll().Select(t => new TeacherResponse(t)));
        }

        /// <summary>
        /// Returns teacher with specified ID.
        /// </summary>
        /// <param name="id">Teacher id.</param>
        /// <returns>Teacher with specified ID.</returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public IActionResult Get([FromRoute] int id)
        {
            return this.Ok(new TeacherResponse(teacherRepository.GetById(id)));
        }

        /// <summary>
        /// Creates new student and adds it to the list.
        /// </summary>
        /// <param name="studentRequest">Object of CreateStudentRequest class.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult Post([FromBody] CreateTeacherRequest teacherRequest)
        {
            int id;
            if (teacherRepository.GetAll().Any())
                id = teacherRepository.GetAll().Select(t => t.Id).Max() + 1;
            else
                id = 0;
            Teacher t = new Teacher(id, teacherRequest.Name, teacherRequest.DepartmentName, teacherRequest.Login, teacherRequest.Password);
            if (teacherRepository.Create(t))
            {
                return this.Ok(t);
            }
            else
            {
                return this.Ok(t);
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
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateTeacherRequest request)
        {
            Teacher t = new Teacher(id, teacherRepository.GetById(id).Name, request.DepartmentName, teacherRepository.GetById(id).Login, request.Password);
            if (teacherRepository.Update(t))
            {
                return this.Ok(t);
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
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (teacherRepository.Delete(id))
            {
                return this.Ok("Operation successful");
            }
            else
            {
                return this.Ok("Operation unsuccessful");
            }
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
            Teacher teacher = teacherRepository.GetAll().Where(t => t.Login == username && t.Password == password).FirstOrDefault();
            if (teacher != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, teacher.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, teacher.Role.ToString()),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
