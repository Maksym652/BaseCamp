using BaseCamp;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Api.Requests;
using WebApp.Api.Responses;
using WebApp.Core.Models;
using WebApp.Core.Repositories;
using Xunit;

namespace WebApp.Tests
{
    public class PointsControllerTest
    {
        [Fact]
        public void GetAllPoints_returnsActionResultOkWithAllPoints()
        {
            // Arrange
            var mock = new Mock<IRepository<Point>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestPoints());
            var controller = new PointsController(mock.Object);
            // Act
            var result = controller.GetAllPoints();
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<IEnumerable<PointResponse>>().Should().HaveCount(GetTestPoints().Count);
            result.As<OkObjectResult>().Value.As<IEnumerable<PointResponse>>().Should().BeEquivalentTo(GetTestPoints().Select(p => new PointResponse(p)));
        }

        [Fact]
        public void GetPointById_whenSpecifiedPointExists_returnsOkResultWithSpecifiedPoint()
        {
            // Arrange
            int testPointId = 1;
            var mock = new Mock<IRepository<Point>>();
            mock.Setup(repo => repo.GetById(testPointId)).Returns(GetTestPoints()[0]);
            var controller = new PointsController(mock.Object);
            // Act
            var result = controller.GetPointById(testPointId);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<PointResponse>().Should().BeEquivalentTo(new PointResponse(GetTestPoints()[0]));
        }

        [Fact]
        public void GetPointById_whenSpecifiedPointNotExists_returnsNotFoundResult()
        {
            // Arrange
            int testPointId = 0;
            var mock = new Mock<IRepository<Point>>();
            mock.Setup(repo => repo.GetById(testPointId)).Returns(GetTestPoints().FirstOrDefault(p => p.Id == testPointId));
            var controller = new PointsController(mock.Object);
            // Act
            var result = controller.GetPointById(testPointId);
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void AddPoint_whenRequestIsValid_returnsOkResultWithAddedPoint()
        {
            // Arrange
            CreatePointRequest testPointRequest = new CreatePointRequest(88.8f, 21910204, "Object-orientate programing", "Lab work №6");
            int id = ((testPointRequest.StudentId - 20000000) * 100) + 1;
            Point expectedTestPoint = new Point(id, testPointRequest.Mark, testPointRequest.StudentId, testPointRequest.Subject, DateTime.Now, testPointRequest.Task);
            var mock = new Mock<IRepository<Point>>();
            mock.Setup(repo => repo.Create(expectedTestPoint)).Returns(true);
            var controller = new PointsController(mock.Object);
            // Act
            var result = controller.AddPoint(testPointRequest);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<Point>().Should().BeEquivalentTo(expectedTestPoint);
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

        [Fact]
        public void AddPoint_whenRequestIsInvalid_returnsBadRequestResult()
        {
            // Arrange
            CreatePointRequest testPointRequest = new CreatePointRequest(10, 21910204, "Object-orientate programing", "Lab work №6");
            int id = ((testPointRequest.StudentId - 20000000) * 100) + 1;
            Point expectedTestPoint = new Point(id, testPointRequest.Mark, testPointRequest.StudentId, testPointRequest.Subject, DateTime.Now, testPointRequest.Task);
            var mock = new Mock<IRepository<Point>>();
            mock.Setup(repo => repo.Create(expectedTestPoint)).Returns(false);
            var controller = new PointsController(mock.Object);
            // Act
            var result = controller.AddPoint(testPointRequest);
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void UpdatePoint_requestIsValid_returnsOkResultWithUpdatedPoint()
        {
            // Arrange
            UpdatePointRequest testUpdatePointRequest = new UpdatePointRequest(12, "Lab work №6 + extra task");
            Point testPoint = new Point(5, 10, 1910205, "Object-orientate programing", DateTime.Now, "Lab work №6");
            Point updatedTestPoint = new Point(testPoint.Id, testUpdatePointRequest.Mark, testPoint.StudentId, testPoint.Subject, DateTime.Now, testUpdatePointRequest.Task);
            var mock = new Mock<IRepository<Point>>();
            mock.Setup(repo => repo.Update(updatedTestPoint)).Returns(true);
            mock.Setup(repo => repo.GetById(testPoint.Id)).Returns(testPoint);
            var controller = new PointsController(mock.Object);
            // Act
            var result = controller.UpdatePoint(testPoint.Id, testUpdatePointRequest);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.As<Point>().Should().BeEquivalentTo(updatedTestPoint);
        }

        [Fact]
        public void UpdatePoint_specifiedPointNotExists_returnsNotFoundResult()
        {
            // Arrange
            UpdatePointRequest testUpdatePointRequest = new UpdatePointRequest(12, "Lab work №6 + extra task");
            Point testPoint = new Point(5, 10, 1910205, "Object-orientate programing", DateTime.Now, "Lab work №6");
            Point updatedTestPoint = new Point(testPoint.Id, testUpdatePointRequest.Mark, testPoint.StudentId, testPoint.Subject, DateTime.Now, testUpdatePointRequest.Task);
            var mock = new Mock<IRepository<Point>>();
            mock.Setup(repo => repo.Update(updatedTestPoint)).Returns(false);
            mock.Setup(repo => repo.GetById(testPoint.Id)).Returns(testPoint);
            var controller = new PointsController(mock.Object);
            // Act
            var result = controller.UpdatePoint(testPoint.Id, testUpdatePointRequest);
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void DeletePoint_specifiedPointExists_returnsOkResult()
        {
            // Arrange
            var mock = new Mock<IRepository<Point>>();
            mock.Setup(repo => repo.Delete(123)).Returns(true);
            var controller = new PointsController(mock.Object);
            // Act
            var result = controller.DeletePoint(123);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void DeletePoint_specifiedPointNotExists_returnsNotFoundResult()
        {
            // Arrange
            var mock = new Mock<IRepository<Point>>();
            mock.Setup(repo => repo.Delete(123)).Returns(false);
            var controller = new PointsController(mock.Object);
            // Act
            var result = controller.DeletePoint(123);
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
