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
    public class CompteControllerTests
    {
        private readonly Mock<ICompteService> repositoryStub = new();
        [Fact]
        public void GetCompteByID_WithUnexistingCompte_ReturnNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetCompteByID(It.IsAny<string>()))
                .Returns((Compte)null);
            var controller = new CompteController(repositoryStub.Object);
            // Act
            var result = controller.GetCompteByID("2");
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetComptes_WithExistingCompte_ReturnComptes()
        {
            // Arrange
            List<Compte> expectedCompte = new List<Compte>();
            expectedCompte.Add(CreateRandomCompte());
            expectedCompte.Add(CreateRandomCompte());
            repositoryStub.Setup(repo => repo.GetComptes())
                .Returns(expectedCompte);
            var controller = new CompteController(repositoryStub.Object);
            // Act
            var actualCompte = controller.GetComptes();
            // Assert
            actualCompte.Should().BeEquivalentTo(
                expectedCompte,
                options => options.ComparingByMembers<Compte>()
            );
        }

        [Fact]
        public void CreateCompte_WithCompteToCreate_ReturnsCreatedCompte()
        {
            // Arrange
            var compteToCreate = CreateRandomCompte();
            
            var controller = new CompteController(repositoryStub.Object);
            // Act
            ActionResult<Compte> result = controller.AddCompte(compteToCreate);
            // Assert
            var createdCompte = result.Value;
            compteToCreate.Should().BeEquivalentTo(
                createdCompte,
                options => options.ComparingByMembers<Compte>().ExcludingMissingMembers()
            );
            createdCompte.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void UpdateCompte_WithCompteToUpdate_ReturnsUpdatedCompte()
        {
            // Arrange
            var existingCompte = CreateRandomCompte();
            repositoryStub.Setup(repo => repo.GetCompteByID(It.IsAny<string>()))
                .Returns(existingCompte);
            var compteToUpdate = new Compte()
            {
                Id = existingCompte.Id,
                Nom = "Test"
            };
            var controller = new CompteController(repositoryStub.Object);
            // Act
            ActionResult<Compte> result = controller.UpdateCompte(compteToUpdate);
            // Assert
            var updatedCompte = result.Value;
            updatedCompte.Should().BeEquivalentTo(
                compteToUpdate,
                options => options.ComparingByMembers<Compte>().ExcludingMissingMembers()
            );
            updatedCompte.Id.Should().NotBeEmpty();
            compteToUpdate.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void DeleteCompte_WithCompteToDelete_ReturnsId()
        {
            // Arrange
            var existingCompte = CreateRandomCompte();
            repositoryStub.Setup(repo => repo.GetCompteByID(It.IsAny<string>()))
                .Returns(existingCompte);
            var controller = new CompteController(repositoryStub.Object);
            // Act
            ActionResult<string> result = controller.DeleteCompte(existingCompte.Id);
            // Assert
            var deletedCompte = result.Value;
            deletedCompte.Should().Be(existingCompte.Id);            
        }

        private Compte CreateRandomCompte()
        {
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                Nom = Guid.NewGuid().ToString()
            };
        }
    }
}
