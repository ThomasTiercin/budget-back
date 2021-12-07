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
    public class CategorieControllerTests
    {
        private readonly Mock<ICategorieService> repositoryStub = new();
        [Fact]
        public void GetCategorieByID_WithUnexistingCategorie_ReturnNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetCategorieByID(It.IsAny<string>()))
                .Returns((Categorie)null);
            var controller = new CategorieController(repositoryStub.Object);
            // Act
            var result = controller.GetCategorieByID("2");
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCategories_WithExistingCategorie_ReturnCategories()
        {
            // Arrange
            List<Categorie> expectedCategorie = new List<Categorie>();
            expectedCategorie.Add(CreateRandomCategorie());
            expectedCategorie.Add(CreateRandomCategorie());
            repositoryStub.Setup(repo => repo.GetCategories())
                .Returns(expectedCategorie);
            var controller = new CategorieController(repositoryStub.Object);
            // Act
            var actualCategorie = controller.GetCategories();
            // Assert
            actualCategorie.Should().BeEquivalentTo(
                expectedCategorie,
                options => options.ComparingByMembers<Categorie>()
            );
        }

        [Fact]
        public void CreateCategorie_WithCategorieToCreate_ReturnsCreatedCategorie()
        {
            // Arrange
            var categorieToCreate = CreateRandomCategorie();
            
            var controller = new CategorieController(repositoryStub.Object);
            // Act
            ActionResult<Categorie> result = controller.AddCategorie(categorieToCreate);
            // Assert
            var createdCategorie = result.Value;
            categorieToCreate.Should().BeEquivalentTo(
                createdCategorie,
                options => options.ComparingByMembers<Categorie>().ExcludingMissingMembers()
            );
            createdCategorie.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void UpdateCategorie_WithCategorieToUpdate_ReturnsUpdatedCategorie()
        {
            // Arrange
            var existingCategorie = CreateRandomCategorie();
            repositoryStub.Setup(repo => repo.GetCategorieByID(It.IsAny<string>()))
                .Returns(existingCategorie);
            var categorieToUpdate = new Categorie()
            {
                Id = existingCategorie.Id,
                Nom = "Test"
            };
            var controller = new CategorieController(repositoryStub.Object);
            // Act
            ActionResult<Categorie> result = controller.UpdateCategorie(categorieToUpdate);
            // Assert
            var updatedCategorie = result.Value;
            updatedCategorie.Should().BeEquivalentTo(
                categorieToUpdate,
                options => options.ComparingByMembers<Categorie>().ExcludingMissingMembers()
            );
            updatedCategorie.Id.Should().NotBeEmpty();
            categorieToUpdate.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void DeleteCategorie_WithCategorieToDelete_ReturnsId()
        {
            // Arrange
            var existingCategorie = CreateRandomCategorie();
            repositoryStub.Setup(repo => repo.GetCategorieByID(It.IsAny<string>()))
                .Returns(existingCategorie);
            var controller = new CategorieController(repositoryStub.Object);
            // Act
            ActionResult<string> result = controller.DeleteCategorie(existingCategorie.Id);
            // Assert
            var deletedCategorie = result.Value;
            deletedCategorie.Should().Be(existingCategorie.Id);            
        }

        private Categorie CreateRandomCategorie()
        {
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                Nom = Guid.NewGuid().ToString()
            };
        }
    }
}
