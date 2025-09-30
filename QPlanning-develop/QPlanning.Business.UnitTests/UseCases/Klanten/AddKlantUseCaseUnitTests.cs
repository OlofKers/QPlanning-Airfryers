using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using QPlanning.Business.Dto.Base.UseCaseResponses;
using QPlanning.Business.Interfaces.Services.Domain;
using QPlanning.Business.UseCases.Klanten.Add;
using QPlanning.Business.UseCases.Klanten.Add.Dto.Commands;
using QPlanning.Business.UseCases.Klanten.Edit;
using QPlanning.Business.UseCases.Klanten.Edit.Dto.Commands;

public class AddKlantUseCaseUnitTests
{
    [Fact]
    public async Task Handle_Should_Fail_With_Negative_Budget()
    {
        // Arrange
        var mockService = new Mock<IKlantDomainService>();
        // Simuleer huidige situatie: service geeft altijd success = true terug
        mockService.Setup(s => s.AddKlant(It.IsAny<AddKlantCommand>()))
            .ReturnsAsync(new BaseResponse("", true));
        var useCase = new AddKlantUseCase(mockService.Object);
        var command = new AddKlantCommand
        {
            Naam = "TestKlant",
            Boekjaar = 2025,
            Budget = -10
        };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        // Test faalt als de actie tóch lukt (want dat mag niet)
        Assert.False(result.Success);
    }

    [Fact]
    public async Task Handle_Should_Fail_With_Default_Budget()
    {
        // Arrange
        var mockService = new Mock<IKlantDomainService>();
        mockService.Setup(s => s.AddKlant(It.IsAny<AddKlantCommand>()))
            .ReturnsAsync(new BaseResponse("", true));
        var useCase = new AddKlantUseCase(mockService.Object);
        var command = new AddKlantCommand
        {
            Naam = "TestKlant",
            Boekjaar = 2025,
            // Budget not set, defaults to 0
        };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task Handle_Should_Fail_With_Empty_Naam()
    {
        // Arrange
        var mockService = new Mock<IKlantDomainService>();
        mockService.Setup(s => s.AddKlant(It.IsAny<AddKlantCommand>()))
            .ReturnsAsync(new BaseResponse("", true));
        var useCase = new AddKlantUseCase(mockService.Object);
        var command = new AddKlantCommand
        {
            Naam = "",
            Boekjaar = 2025,
            Budget = 1000
        };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task EditKlant_Handle_Should_Succeed()
    {
        // Arrange
        var mockService = new Mock<IKlantDomainService>();
        mockService.Setup(s => s.EditKlant(It.IsAny<EditKlantCommand>()))
            .ReturnsAsync(new BaseResponse("", true));
        var useCase = new EditKlantUseCase(mockService.Object);
        var command = new EditKlantCommand
        {
            Id = 1,
            Naam = "TestKlant"
        };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
    }
}