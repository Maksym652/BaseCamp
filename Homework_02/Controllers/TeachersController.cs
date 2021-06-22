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
        public IActionResult GetAllTeachers()
        {
            return Ok(teacherRepository.GetAll()
                .Select(t => new TeacherResponse(t)));
        }

        /// <summary>
        /// Returns teacher with specified ID.
        /// </summary>
        /// <param name="id">Teacher id.</param>
        /// <returns>Teacher with specified ID.</returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public IActionResult GetTeacherById([FromRoute] int id)
        {
            Teacher t = teacherRepository.GetById(id);
            if (t == null)
            {
                return NotFound("Teacher with specified id not found.");
            }

            return Ok(new TeacherResponse(t));
        }

        /// <summary>
        /// Creates new teacher and adds it to the list.
        /// </summary>
        /// <param name="teacherRequest">Object of CreateTeacherRequest class.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult AddTeacher([FromBody] CreateTeacherRequest teacherRequest)
        {
            int id;
            if (teacherRepository.GetAll().Any())
                id = teacherRepository.GetAll()
                    .Select(t => t.Id).Max() + 1;
            else
                id = 0;
            Teacher t = new Teacher(id, teacherRequest.Name, teacherRequest.DepartmentName, teacherRequest.Login, teacherRequest.Password);
            if (teacherRepository.Create(t))
            {
                return Ok(t);
            }
            else
            {
                return BadRequest(t);
            }
        }

        /// <summary>
        /// Updates teacher with specified ID.
        /// </summary>
        /// <param name="id">teacher id.</param>
        /// <param name="request">Update teacher request.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        [Route("{id}")]
        public IActionResult UpdateTeacher([FromRoute] int id, [FromBody] UpdateTeacherRequest request)
        {
            Teacher beforeUpdating = teacherRepository.GetById(id);
            if (beforeUpdating == null)
            {
                return BadRequest("Teacher with specified ID not found.");
            }

            Teacher updated = new Teacher(id, beforeUpdating.Name, request.DepartmentName, beforeUpdating.Login, request.Password);
            if (teacherRepository.Update(updated))
            {
                return Ok(updated);
            }
            else
            {
                return BadRequest("Operation unsuccessful");
            }
        }

        /// <summary>
        /// Removes teacher with specified ID.
        /// </summary>
        /// <param name="id">Teacher id.</param>
        /// <returns>Object that represents result of the method work.</returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult DeleteTeacher([FromRoute] int id)
        {
            if (teacherRepository.Delete(id))
            {
                return Ok("Operation successful");
            }
            else
            {
                return NotFound("Teacher with specified ID not found.");
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
            Teacher teacher = teacherRepository.GetAll()
                .Where(t => t.Login == username && t.Password == password).FirstOrDefault();
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
