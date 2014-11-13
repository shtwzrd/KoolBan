using System;
using System.Collections.Generic;
using KoolBan.Controllers;
using KoolBan.Models;
using KoolBan.Models.Abstract;
using Moq;
using NUnit.Framework;

namespace KoolBan.Tests.ControllerTests
{
    [TestFixture]
    public class ProjectsControllerTests
    {
        [Test]
        public void Test_Create()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            ProjectsController controller = new ProjectsController(projectRepositoryMock.Object);

            Project project = new Project
            {
                ProjectId = "1",
                IsPrivate = false
            };

            // Act
            controller.CreateProject(project);

            // Assert
            projectRepositoryMock.Verify(x => x.Create(project));
            projectRepositoryMock.Verify(x => x.Save());
        }

        [Test]
        public void Test_Update()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            ProjectsController controller = new ProjectsController(projectRepositoryMock.Object);

            Project project = new Project
            {
                ProjectId = "1",
                IsPrivate = false
            };

            // Act
            controller.UpdateProject(project);

            // Assert
            projectRepositoryMock.Verify(x => x.Edit(project));
            projectRepositoryMock.Verify(x => x.Save());
        }

        // TODO: Not working. Figure out how to test Read
//        [Test]
//        public void Test_Read()
//        {
//            // Arrange
//            var projectRepositoryMock = new Mock<IProjectRepository>();
//
//            ProjectsController controller = new ProjectsController(projectRepositoryMock.Object);
//
//            // Act
//            controller.ReadProject("projectName");
//
//            // Assert
//            projectRepositoryMock.Verify(x => x.Find("projectName"));
//        }
    }
}
