using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Budget.Api.Controllers;
using Budget.Api.Services;
using Budget.Api.Models;
using FluentAssertions;
using System.Collections.Generic;

namespace Budget.UnitTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> repositoryStub = new();
        [Fact]
        public void GetUserByID_WithUnexistingUser_ReturnNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetUserByID(It.IsAny<int>()))
                .Returns((User)null);
            var controller = new UserController(repositoryStub.Object);
            // Act
            var result = controller.GetUserByID(2);
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetUsers_WithExistingUser_ReturnUsers()
        {
            // Arrange
            List<User> expectedUser = new List<User>();
            expectedUser.Add(CreateRandomUser());
            expectedUser.Add(CreateRandomUser());
            repositoryStub.Setup(repo => repo.GetUsers())
                .Returns(expectedUser);
            var controller = new UserController(repositoryStub.Object);
            // Act
            var actualUser = controller.GetUsers();
            // Assert
            actualUser.Should().BeEquivalentTo(
                expectedUser,
                options => options.ComparingByMembers<User>()
            );
        }

        [Fact]
        public void CreateUser_WithUserToCreate_ReturnsCreatedUser()
        {
            // Arrange
            var userToCreate = CreateRandomUser();
            
            var controller = new UserController(repositoryStub.Object);
            // Act
            ActionResult<User> result = controller.AddUser(userToCreate);
            // Assert
            var createdUser = result.Value;
            userToCreate.Should().BeEquivalentTo(
                createdUser,
                options => options.ComparingByMembers<User>().ExcludingMissingMembers()
            );
            createdUser.Id.Should().BePositive();
        }

        [Fact]
        public void UpdateUser_WithUserToUpdate_ReturnsUpdatedUser()
        {
            // Arrange
            var existingUser = CreateRandomUser();
            repositoryStub.Setup(repo => repo.GetUserByID(It.IsAny<int>()))
                .Returns(existingUser);
            var userToUpdate = new User()
            {
                Id = existingUser.Id,
                UserName = "Test",
                Password = "password",
                Role = "User"
            };
            var controller = new UserController(repositoryStub.Object);
            // Act
            ActionResult<User> result = controller.UpdateUser(userToUpdate);
            // Assert
            var updatedUser = result.Value;
            updatedUser.Should().BeEquivalentTo(
                userToUpdate,
                options => options.ComparingByMembers<User>().ExcludingMissingMembers()
            );
            updatedUser.Id.Should().BePositive();
            userToUpdate.Id.Should().BePositive();
        }

        [Fact]
        public void DeleteUser_WithUserToDelete_ReturnsId()
        {
            // Arrange
            var existingUser = CreateRandomUser();
            repositoryStub.Setup(repo => repo.GetUserByID(It.IsAny<int>()))
                .Returns(existingUser);
            var controller = new UserController(repositoryStub.Object);
            // Act
            ActionResult<int> result = controller.DeleteUser(existingUser.Id);
            // Assert
            var deletedUser = result.Value;
            deletedUser.Should().Be(existingUser.Id);            
        }

        private User CreateRandomUser()
        {
            return new()
            {
                Id = new Random().Next(1, 1000000),
                UserName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                Role = "User"
            };
        }
    }
}
