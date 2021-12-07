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
    public class TypeControllerTests
    {
        private readonly Mock<ITypeService> repositoryStub = new();
        [Fact]
        public void GetTypeByID_WithUnexistingType_ReturnNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetTypeByID(It.IsAny<string>()))
                .Returns((Budget.Api.Models.Type)null);
            var controller = new TypeController(repositoryStub.Object);
            // Act
            var result = controller.GetTypeByID("2");
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetTypes_WithExistingType_ReturnTypes()
        {
            // Arrange
            List<Budget.Api.Models.Type> expectedType = new List<Budget.Api.Models.Type>();
            expectedType.Add(CreateRandomType());
            expectedType.Add(CreateRandomType());
            repositoryStub.Setup(repo => repo.GetTypes())
                .Returns(expectedType);
            var controller = new TypeController(repositoryStub.Object);
            // Act
            var actualType = controller.GetTypes();
            // Assert
            actualType.Should().BeEquivalentTo(
                expectedType,
                options => options.ComparingByMembers<Budget.Api.Models.Type>()
            );
        }

        [Fact]
        public void CreateType_WithTypeToCreate_ReturnsCreatedType()
        {
            // Arrange
            var typeToCreate = CreateRandomType();
            
            var controller = new TypeController(repositoryStub.Object);
            // Act
            ActionResult<Budget.Api.Models.Type> result = controller.AddType(typeToCreate);
            // Assert
            var createdType = result.Value;
            typeToCreate.Should().BeEquivalentTo(
                createdType,
                options => options.ComparingByMembers<Budget.Api.Models.Type>().ExcludingMissingMembers()
            );
            createdType.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void UpdateType_WithTypeToUpdate_ReturnsUpdatedType()
        {
            // Arrange
            var existingType = CreateRandomType();
            repositoryStub.Setup(repo => repo.GetTypeByID(It.IsAny<string>()))
                .Returns(existingType);
            var typeToUpdate = new Budget.Api.Models.Type()
            {
                Id = existingType.Id,
                Nom = "Test"
            };
            var controller = new TypeController(repositoryStub.Object);
            // Act
            ActionResult<Budget.Api.Models.Type> result = controller.UpdateType(typeToUpdate);
            // Assert
            var updatedType = result.Value;
            updatedType.Should().BeEquivalentTo(
                typeToUpdate,
                options => options.ComparingByMembers<Budget.Api.Models.Type>().ExcludingMissingMembers()
            );
            updatedType.Id.Should().NotBeEmpty();
            typeToUpdate.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void DeleteType_WithTypeToDelete_ReturnsId()
        {
            // Arrange
            var existingType = CreateRandomType();
            repositoryStub.Setup(repo => repo.GetTypeByID(It.IsAny<string>()))
                .Returns(existingType);
            var controller = new TypeController(repositoryStub.Object);
            // Act
            ActionResult<string> result = controller.DeleteType(existingType.Id);
            // Assert
            var deletedType = result.Value;
            deletedType.Should().Be(existingType.Id);            
        }

        private Budget.Api.Models.Type CreateRandomType()
        {
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                Nom = Guid.NewGuid().ToString()
            };
        }
    }
}
