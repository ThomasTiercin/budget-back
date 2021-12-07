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
    public class OrganismeControllerTests
    {
        private readonly Mock<IOrganismeService> repositoryStub = new();
        [Fact]
        public void GetOrganismeByID_WithUnexistingOrganisme_ReturnNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetOrganismeByID(It.IsAny<string>()))
                .Returns((Organisme)null);
            var controller = new OrganismeController(repositoryStub.Object);
            // Act
            var result = controller.GetOrganismeByID("2");
            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetOrganismes_WithExistingOrganisme_ReturnOrganismes()
        {
            // Arrange
            List<Organisme> expectedOrganisme = new List<Organisme>();
            expectedOrganisme.Add(CreateRandomOrganisme());
            expectedOrganisme.Add(CreateRandomOrganisme());
            repositoryStub.Setup(repo => repo.GetOrganismes())
                .Returns(expectedOrganisme);
            var controller = new OrganismeController(repositoryStub.Object);
            // Act
            var actualOrganisme = controller.GetOrganismes();
            // Assert
            actualOrganisme.Should().BeEquivalentTo(
                expectedOrganisme,
                options => options.ComparingByMembers<Organisme>()
            );
        }

        [Fact]
        public void CreateOrganisme_WithOrganismeToCreate_ReturnsCreatedOrganisme()
        {
            // Arrange
            var organismeToCreate = CreateRandomOrganisme();
            
            var controller = new OrganismeController(repositoryStub.Object);
            // Act
            ActionResult<Organisme> result = controller.AddOrganisme(organismeToCreate);
            // Assert
            var createdOrganisme = result.Value;
            organismeToCreate.Should().BeEquivalentTo(
                createdOrganisme,
                options => options.ComparingByMembers<Organisme>().ExcludingMissingMembers()
            );
            createdOrganisme.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void UpdateOrganisme_WithOrganismeToUpdate_ReturnsUpdatedOrganisme()
        {
            // Arrange
            var existingOrganisme = CreateRandomOrganisme();
            repositoryStub.Setup(repo => repo.GetOrganismeByID(It.IsAny<string>()))
                .Returns(existingOrganisme);
            var organismeToUpdate = new Organisme()
            {
                Id = existingOrganisme.Id,
                Nom = "Test"
            };
            var controller = new OrganismeController(repositoryStub.Object);
            // Act
            ActionResult<Organisme> result = controller.UpdateOrganisme(organismeToUpdate);
            // Assert
            var updatedOrganisme = result.Value;
            updatedOrganisme.Should().BeEquivalentTo(
                organismeToUpdate,
                options => options.ComparingByMembers<Organisme>().ExcludingMissingMembers()
            );
            updatedOrganisme.Id.Should().NotBeEmpty();
            organismeToUpdate.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void DeleteOrganisme_WithOrganismeToDelete_ReturnsId()
        {
            // Arrange
            var existingOrganisme = CreateRandomOrganisme();
            repositoryStub.Setup(repo => repo.GetOrganismeByID(It.IsAny<string>()))
                .Returns(existingOrganisme);
            var controller = new OrganismeController(repositoryStub.Object);
            // Act
            ActionResult<string> result = controller.DeleteOrganisme(existingOrganisme.Id);
            // Assert
            var deletedOrganisme = result.Value;
            deletedOrganisme.Should().Be(existingOrganisme.Id);            
        }

        private Organisme CreateRandomOrganisme()
        {
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                Nom = Guid.NewGuid().ToString()
            };
        }
    }
}
