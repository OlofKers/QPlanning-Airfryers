using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using QPlanning.Business.Domain.Entities;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.UseCases.Boeking.Add;
using QPlanning.Business.UseCases.Boeking.Add.Dto;

public class BoekingUseCaseUnitTests
{
    [Fact]
    public async Task AddBoekingen_With_Empty_List_Should_Fail()
    {
        var mockService = new Mock<IBoekingService>();
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand { MedewerkerIds = new List<int>(), Jaar = 2025, Uren = 5, KlantId = 1, Weeknummer = 1 };

        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
        Assert.Equal("Geen medewerker opgegeven", result.Message);
    }

    [Fact]
    public async Task AddBoeking_With_Negative_Uren_Should_Fail()
    {
        var mockService = new Mock<IBoekingService>();
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand
        {
            MedewerkerIds = new List<int> { 1 },
            Jaar = 2025,
            KlantId = 1,
            Weeknummer = 1,
            Uren = -5
        };

        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
        Assert.Equal("Uren kunnen niet negatief zijn", result.Message);
    }

    [Fact]
    public async Task AddBoeking_With_Invalid_KlantId_Should_Fail()
    {
        var mockService = new Mock<IBoekingService>();
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand
        {
            MedewerkerIds = new List<int> { 1 },
            Jaar = 2025,
            KlantId = 0,
            Weeknummer = 1,
            Uren = 5
        };

        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
        Assert.Equal("Ongeldige klantId", result.Message);
    }

    [Fact]
    public async Task AddBoeking_With_Invalid_Weeknummer_Should_Fail()
    {
        var mockService = new Mock<IBoekingService>();
        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand
        {
            MedewerkerIds = new List<int> { 1 },
            Jaar = 2025,
            KlantId = 1,
            Weeknummer = 0,
            Uren = 5
        };

        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
        Assert.Equal("Ongeldige weeknummer", result.Message);
    }

    [Fact]
    public async Task AddBoeking_With_Valid_Input_Should_Succeed()
    {
        var mockService = new Mock<IBoekingService>();
        mockService.Setup(s => s.AddBoekingen(It.IsAny<List<DomainModelBoeking>>()))
                   .ReturnsAsync(new BoekingResponse(1, true, "Success"));

        var useCase = new AddBoekingUseCase(mockService.Object);
        var command = new AddBoekingCommand
        {
            MedewerkerIds = new List<int> { 1 },
            Jaar = 2025,
            KlantId = 1,
            Weeknummer = 1,
            Uren = 8
        };

        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.True(result.Success);
        Assert.Equal("Success", result.Message);
    }
}
