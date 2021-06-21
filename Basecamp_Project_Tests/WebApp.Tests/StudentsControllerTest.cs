using Basecamp;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Core.Models;
using WebApp.Core.Repositories;
using Xunit;
using FluentAssertions;
using System.Collections;
using System.Linq;
using WebApp.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using WebApp.Api.Requests;

namespace WebApp.Tests
{
    public class StudentsControllerTest
    {
        [Fact]
        public void GetAllStudents_returnsActionResultOkWithAllStudents()
        {
            // Arrange
            var mock = new Mock<IRepository<Student>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestStudents());
            var controller = new StudentsController(mock.Object, null);
            // Act
            var result = controller.GetAllStudents();
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<IEnumerable<StudentResponse>>().Should().HaveCount(GetTestStudents().Count());
            result.As<OkObjectResult>().Value.As<IEnumerable<StudentResponse>>().Should().BeEquivalentTo(GetTestStudents().Select(st => new StudentResponse(st)));
        }

        [Fact]
        public void GetStudentById_whenSpecifiedStudentExists_returnsActionResultOkWithStudentWhoHasSpecifiedId()
        {
            // Arrange
            int testStudentId = 21910202;
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.GetById(testStudentId)).Returns(GetTestStudents()[0]);
            var controller = new StudentsController(mockStudentRepository.Object, null);
            // Act
            var result = controller.GetStudentById(testStudentId);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<StudentResponse>().Should().BeEquivalentTo(new StudentResponse(GetTestStudents()[0]));
        }

        [Fact]
        public void GetStudentById_whenSpecifiedStudentNotExists_returnsNotFoundResult()
        {
            // Arrange
            int testStudentId = 12345;
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.GetById(testStudentId)).Returns(GetTestStudents().FirstOrDefault(st => st.Id == testStudentId));
            var controller = new StudentsController(mockStudentRepository.Object, null);
            // Act
            var result = controller.GetStudentById(testStudentId);
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        private List<Student> GetTestStudents()
        {
            return new List<Student>
            {
                new Student("petrenko123", "123abcdef", 21910202, "Петренко П.П.", 302, 122, true ),
                new Student("ivanenkoivan", "a1b2c3d4", 22020202, "Іваненко І.І.", 302, 122, false ),
                new Student("stepanenko2020", "123abcdef", 22010505, "Степаненко С.С.", 105, 123, true ),
                new Student("vasylenkov111", "123abcdef", 21910901, "Василенко В.В.", 309, 122, true )
            };
        }

        [Fact]
        public void AddStudent_whenRequestIsValid_returnsOkResultWithAddedStudent()
        {
            // Arrange
            CreateStudentRequest testStudentRequest = new CreateStudentRequest("testStudent123", "1234abcd", "Іваненко І.І.", 101, 121, true);
            int expectedTestStudentId = 20000000 + ((DateTime.Now.Year - 2000) * 100000) + (testStudentRequest.Group * 100) + 1;
            Student expectedTestStudent = new Student(testStudentRequest.Login, testStudentRequest.Password, expectedTestStudentId, testStudentRequest.Name, testStudentRequest.Group, testStudentRequest.Specialty, testStudentRequest.IsStudiedOnBudget);
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.Create(expectedTestStudent)).Returns(true);
            var controller = new StudentsController(mockStudentRepository.Object, null);
            // Act
            var result = controller.AddStudent(testStudentRequest);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<Student>().Should().BeEquivalentTo(expectedTestStudent);
        }


        [Fact]
        public void AddStudent_whenOtherStudentWithSameIdExists_returnsBadRequestResult()
        {
            // Arrange
            CreateStudentRequest testStudentRequest = new CreateStudentRequest("testStudent123", "1234abcd", "Іваненко І.І.", 101, 121, true);
            int expectedTestStudentId = 20000000 + ((DateTime.Now.Year - 2000) * 100000) + (testStudentRequest.Group * 100) + 1;
            Student expectedTestStudent = new Student(testStudentRequest.Login, testStudentRequest.Password, expectedTestStudentId, testStudentRequest.Name, testStudentRequest.Group, testStudentRequest.Specialty, testStudentRequest.IsStudiedOnBudget);
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.Create(expectedTestStudent)).Returns(false);
            var controller = new StudentsController(mockStudentRepository.Object, null);
            // Act
            var result = controller.AddStudent(testStudentRequest);
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void UpdateStudent_requestIsValid_returnsOkResultWithUpdatedStudent()
        {
            // Arrange
            UpdateStudentRequest testStudentUpdateRequest = new UpdateStudentRequest("123abc", 102, false);
            Student testStudent = new Student("testStudent", "1111", 1910202, "Ivanenko I.I.", 101, 121, true);
            Student updatedTestStudent = new Student(testStudent.Login, testStudentUpdateRequest.NewPassword, testStudent.Id, testStudent.Name, testStudentUpdateRequest.Group, testStudent.Specialty, testStudentUpdateRequest.IsStudiedOnBudget);
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.Update(testStudent)).Returns(true);
            mockStudentRepository.Setup(repo => repo.GetById(testStudent.Id)).Returns(testStudent);
            var controller = new StudentsController(mockStudentRepository.Object, null);
            // Act
            var result = controller.UpdateStudent(1910202, testStudentUpdateRequest);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<Student>().Should().BeEquivalentTo(updatedTestStudent);
        }

        [Fact]
        public void UpdateStudent_specifiedStudentNotExists_returnsBadRequestResult()
        {
            // Arrange
            UpdateStudentRequest testStudentUpdateRequest = new UpdateStudentRequest("123abc", 102, false);
            Student testStudent = new Student("testStudent", "1111", 12345, "Ivanenko I.I.", 101, 121, true);
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.Update(testStudent)).Returns(false);
            mockStudentRepository.Setup(repo => repo.GetById(12345)).Returns(() => null);
            var controller = new StudentsController(mockStudentRepository.Object, null);
            // Act
            var result = controller.UpdateStudent(12345, testStudentUpdateRequest);
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void DeleteStudent_specifiedStudentExists_returnsOkResult()
        {
            // Arrange
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.Delete(123)).Returns(true);
            var controller = new StudentsController(mockStudentRepository.Object, null);
            // Act
            var result = controller.DeleteStudent(123);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void DeleteStudent_specifiedStudentNotExists_returnsNotFoundResult()
        {
            // Arrange
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.Delete(123)).Returns(false);
            var controller = new StudentsController(mockStudentRepository.Object, null);
            // Act
            var result = controller.DeleteStudent(123);
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void GetPoints_specifiedStudentExists_returnsOkResult()
        {
            // Arrange
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.GetById(1910202)).Returns(GetTestStudents()[0]);
            var mockPointRepository = new Mock<IRepository<Point>>();
            mockPointRepository.Setup(repo => repo.GetAll()).Returns(GetTestPoints());
            var controller = new StudentsController(mockStudentRepository.Object, mockPointRepository.Object);
            // Act
            var result = controller.GetPoints(1910202);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<IEnumerable<PointResponse>>().Should().BeEquivalentTo(GetTestPoints().Where(p => p.StudentId == 1910202).Select(p => new PointResponse(p)));
        }

        [Fact]
        public void GetPoints_specifiedStudentNotExists_returnsOkResult()
        {
            // Arrange
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.GetById(1910203)).Returns(() => null);
            var mockPointRepository = new Mock<IRepository<Point>>();
            mockPointRepository.Setup(repo => repo.GetAll()).Returns(new List<Point>());
            var controller = new StudentsController(mockStudentRepository.Object, mockPointRepository.Object);
            // Act
            var result = controller.GetPoints(1910203);
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void Token_whenSpecifiedStudentExists_returnsJsonresult()
        {
            // Arrange
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.GetAll()).Returns(GetTestStudents());
            var controller = new StudentsController(mockStudentRepository.Object, null);
            // Act
            var result = controller.Token("petrenko123", "123abcdef");
            // Assert
            result.Should().BeOfType<JsonResult>();
        }

        [Fact]
        public void Token_whenSpecifiedStudentNotExists_returnsBadRequestResult()
        {
            // Arrange
            var mockStudentRepository = new Mock<IRepository<Student>>();
            mockStudentRepository.Setup(repo => repo.GetAll()).Returns(GetTestStudents());
            var controller = new StudentsController(mockStudentRepository.Object, null);
            // Act
            var result = controller.Token("non-existant_student", "12345678");
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        private List<Point> GetTestPoints()
        {
            return new List<Point>
            {
                new Point(1, 78, 1910202, "Object-orientate programing", new DateTime(2021, 6, 10), "Exam"),
                new Point(2, 95, 1910202, "Object-orientate programing", new DateTime(2021, 3, 10), "Course-project"),
                new Point(3, 85, 1910101, "Object-orientate programing", new DateTime(2021, 6, 10), "Exam"),
                new Point(4, 100, 1910101, "Object-orientate programing", new DateTime(2021, 3, 10), "Course-project"),
            };
        }
    }
}
