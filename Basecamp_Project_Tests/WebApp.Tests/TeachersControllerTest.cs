﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Api.Controllers;
using WebApp.Api.Requests;
using WebApp.Api.Responses;
using WebApp.Core.Models;
using WebApp.Core.Repositories;
using Xunit;

namespace WebApp.Tests
{
    public class TeachersControllerTest
    {
        [Fact]
        public void GetAllTeachers_returnsActionResultOkWithAllTeachers()
        {
            // Arrange
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestTeachers());
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.GetAllTeachers();
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<IEnumerable<TeacherResponse>>().Should().HaveCount(GetTestTeachers().Count());
            result.As<OkObjectResult>().Value.As<IEnumerable<TeacherResponse>>().Should().BeEquivalentTo(GetTestTeachers().Select(t => new TeacherResponse(t)));
        }

        [Fact]
        public void GetTeacherById_whenSpecifiedTeacherExists_returnsActionResultOkWithTeachertWhoHasSpecifiedId()
        {
            // Arrange
            int testTeacherId = 1;
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.GetById(testTeacherId)).Returns(GetTestTeachers()[0]);
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.GetTeacherById(testTeacherId);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<TeacherResponse>().Should().BeEquivalentTo(new TeacherResponse(GetTestTeachers()[0]));
        }

        [Fact]
        public void GetTeacherById_whenSpecifiedTeacherNotExists_returnsNotFoundResult()
        {
            // Arrange
            int testTeacherId = 1;
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.GetById(testTeacherId)).Returns(() => null);
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.GetTeacherById(testTeacherId);
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        private List<Teacher> GetTestTeachers()
        {
            return new List<Teacher>
            {
                new Teacher(1, "Borovliova S.U.", "Computer Science Faculty", "BSU", "12345678"),
                new Teacher(2, "Nezdoliy U.O.", "Computer Science Faculty", "NUO", "abcdefgh"),
                new Teacher(2, "Dvoretska S.V.", "Computer Science Faculty", "DSV", "1a2b3c4d"),
            };
        }

        [Fact]
        public void AddTeacher_whenRequestIsValid_returnsOkResultWithAddedTeacher()
        {
            // Arrange
            CreateTeacherRequest testTeacherRequest = new CreateTeacherRequest("Best_teacher_123", "123abc", "Teacher T.T.", "Computer Science Faculty");
            Teacher expectedTestTeacher = new Teacher(0, testTeacherRequest.Name, testTeacherRequest.DepartmentName, testTeacherRequest.Login, testTeacherRequest.Password);
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.Create(expectedTestTeacher)).Returns(true);
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.AddTeacher(testTeacherRequest);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<Teacher>().Should().BeEquivalentTo(expectedTestTeacher);
        }


        [Fact]
        public void AddTeacher_whenRequestIsInvalid_returnsBadRequestResult()
        {
            // Arrange
            CreateTeacherRequest testTeacherRequest = new CreateTeacherRequest("Best_teacher_123", "123abc", "Teacher T.T.", "Computer Science Faculty");
            Teacher expectedTestTeacher = new Teacher(0, testTeacherRequest.Name, testTeacherRequest.DepartmentName, testTeacherRequest.Login, testTeacherRequest.Password);
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.Create(expectedTestTeacher)).Returns(false);
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.AddTeacher(testTeacherRequest);
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void UpdateTeacher_requestIsValid_returnsOkResultWithUpdatedTeacher()
        {
            // Arrange
            UpdateTeacherRequest testUpdateTeacherRequest = new UpdateTeacherRequest("123456", "IIS Department of Computer Science Faculty");
            Teacher testTeacher = new Teacher(5, "Teacher T.T.", "Computer Science Faculty", "Best_teacher_123", "123");
            Teacher updatedTestTeacher = new Teacher(testTeacher.Id, testTeacher.Name, testUpdateTeacherRequest.DepartmentName, testTeacher.Login, testUpdateTeacherRequest.Password);
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.Update(testTeacher)).Returns(true);
            mock.Setup(repo => repo.GetById(testTeacher.Id)).Returns(testTeacher);
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.UpdateTeacher(testTeacher.Id, testUpdateTeacherRequest);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<Teacher>().Should().BeEquivalentTo(updatedTestTeacher);
        }

        [Fact]
        public void UpdateTeacher_specifiedTeacherNotExists_returnsBadRequestResult()
        {
            // Arrange
            UpdateTeacherRequest testUpdateTeacherRequest = new UpdateTeacherRequest("123456", "IIS Department of Computer Science Faculty");
            Teacher testTeacher = new Teacher(5, "Teacher T.T.", "Computer Science Faculty", "Best_teacher_123", "123");
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.Update(testTeacher)).Returns(false);
            mock.Setup(repo => repo.GetById(testTeacher.Id)).Returns(testTeacher);
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.UpdateTeacher(testTeacher.Id, testUpdateTeacherRequest);
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void DeleteTeacher_specifiedTeacherExists_returnsOkResult()
        {
            // Arrange
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.Delete(123)).Returns(true);
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.DeleteTeacher(123);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void DeleteTeacher_specifiedTeacherNotExists_returnsNotFoundResult()
        {
            // Arrange
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.Delete(123)).Returns(false);
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.DeleteTeacher(123);
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void Token_whenSpecifiedTeacherExists_returnsJsonresult()
        {
            // Arrange
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestTeachers());
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.Token("BSU", "12345678");
            // Assert
            result.Should().BeOfType<JsonResult>();
        }

        [Fact]
        public void Token_whenSpecifiedTeacherNotExists_returnsBadRequestResult()
        {
            // Arrange
            var mock = new Mock<IRepository<Teacher>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestTeachers());
            var controller = new TeachersController(mock.Object);
            // Act
            var result = controller.Token("non-existant_teacher", "12345678");
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
