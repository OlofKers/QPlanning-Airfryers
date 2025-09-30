using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using QPlanning.Business.Domain.Entities;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.UseCases.Boeking.Add;
using QPlanning.Business.UseCases.Boeking.Add.Dto;
using QPlanning.Business.UseCases.Boeking.Dto;
using QPlanning.Business.UseCases.Boeking.Update;
using QPlanning.Business.UseCases.Boeking.Update.Dto;

public class BoekingUseCaseUnitTests
{
    [Fact]
    public async Task AddBoekingen_With_Empty_List_Should_Fail()
    {
        // Arrange
        var mockService = new Mock<IBoekingService>();
        // Simuleer huidige situatie: service geeft altijd success = true terug
        mockService.Setup(s => s.AddBoekingen(It.IsAny<List<DomainModelBoeking>>()))
            .ReturnsAsync(new BoekingResponse(1, true, "Success"));
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand { MedewerkerIds = new List<int>(), Jaar = 2025 };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        // Test faalt als de actie tóch lukt (want dat mag niet)
        Assert.False(result.Success);
    }

    [Fact]
    public async Task UpdateBoeking_With_Invalid_MedewerkerId_Should_Fail()
    {
        // Arrange
        var mockService = new Mock<IBoekingService>();
        mockService.Setup(s => s.UpdateBoeking(It.IsAny<DomainModelBoeking>()))
            .ReturnsAsync(new BoekingResponse(1, true, "Success"));
        var useCase = new UpdateBoekingUseCase(mockService.Object);
        var command = new UpdateBoekingCommand { Jaar = 2025, MedewerkerId = 0 };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task AddBoeking_With_Negative_Uren_Should_Fail()
    {
        // Arrange
        var mockService = new Mock<IBoekingService>();
        mockService.Setup(s => s.AddBoekingen(It.IsAny<List<DomainModelBoeking>>()))
            .ReturnsAsync(new BoekingResponse(1, true, "Success"));
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand
        {
            MedewerkerIds = new List<int> { 1 },
            Jaar = 2025,
            Uren = -5 // Negatieve uren
        };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task AddBoeking_With_Invalid_KlantId_Should_Fail()
    {
        // Arrange
        var mockService = new Mock<IBoekingService>();
        mockService.Setup(s => s.AddBoekingen(It.IsAny<List<DomainModelBoeking>>()))
            .ReturnsAsync(new BoekingResponse(1, true, "Success"));
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand
        {
            MedewerkerIds = new List<int> { 1 },
            Jaar = 2025,
            KlantId = 0 // Ongeldige klantId
        };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task AddBoeking_With_Invalid_Weeknummer_Should_Fail()
    {
        // Arrange
        var mockService = new Mock<IBoekingService>();
        mockService.Setup(s => s.AddBoekingen(It.IsAny<List<DomainModelBoeking>>()))
            .ReturnsAsync(new BoekingResponse(1, true, "Success"));
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand
        {
            MedewerkerIds = new List<int> { 1 },
            Jaar = 2025,
            Weeknummer = 0 // Ongeldige weeknummer
        };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task AddBoeking_With_Missing_MedewerkerId_Should_Fail()
    {
        // Arrange
        var mockService = new Mock<IBoekingService>();
        mockService.Setup(s => s.AddBoekingen(It.IsAny<List<DomainModelBoeking>>()))
            .ReturnsAsync(new BoekingResponse(1, true, "Success"));
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand
        {
            Jaar = 2025,
            // MedewerkerIds niet gezet en MedewerkerId niet gezet
        };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
    }
}