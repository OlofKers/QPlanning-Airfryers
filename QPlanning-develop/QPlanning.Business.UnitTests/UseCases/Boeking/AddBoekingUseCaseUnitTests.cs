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
    public async Task AddBoeking_Handle_Should_Succeed()
    {
        // Arrange
        var mockService = new Mock<IBoekingService>();
        mockService.Setup(s => s.AddBoekingen(It.IsAny<List<DomainModelBoeking>>()))
            .ReturnsAsync(new BoekingResponse(1, true, "Success"));
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand { MedewerkerIds = new List<int> { 1 }, Jaar = 2025 };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateBoeking_Handle_Should_Succeed()
    {
        // Arrange
        var mockService = new Mock<IBoekingService>();
        mockService.Setup(s => s.UpdateBoeking(It.IsAny<DomainModelBoeking>()))
            .ReturnsAsync(new BoekingResponse(1, true, "Success"));
        var useCase = new UpdateBoekingUseCase(mockService.Object);
        var command = new UpdateBoekingCommand { Jaar = 2025, MedewerkerId = 1 };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task AddBoekingen_With_Empty_List_Should_Fail()
    {
        // Arrange
        var mockService = new Mock<IBoekingService>();
        mockService.Setup(s => s.AddBoekingen(It.Is<List<DomainModelBoeking>>(l => l.Count == 0)))
            .ReturnsAsync(new BoekingResponse(0, false, "Niet toegestaan"));
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand { MedewerkerIds = new List<int>(), Jaar = 2025 };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task UpdateBoeking_With_Invalid_MedewerkerId_Should_Fail()
    {
        // Arrange
        var mockService = new Mock<IBoekingService>();
        mockService.Setup(s => s.UpdateBoeking(It.Is<DomainModelBoeking>(b => b.MedewerkerId == 0)))
            .ReturnsAsync(new BoekingResponse(0, false, "Niet toegestaan"));
        var useCase = new UpdateBoekingUseCase(mockService.Object);
        var command = new UpdateBoekingCommand { Jaar = 2025, MedewerkerId = 0 };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
    }
}