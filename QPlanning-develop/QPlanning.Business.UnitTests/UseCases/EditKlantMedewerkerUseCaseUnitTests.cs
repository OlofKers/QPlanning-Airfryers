using Moq;
using QPlanning.Business.Dto.Base.UseCaseResponses;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.Interfaces.Services.Domain;
using QPlanning.Business.UseCases.Klanten.Edit;
using QPlanning.Business.UseCases.Klanten.Edit.Dto.Commands;
using QPlanning.Business.UseCases.Medewerkers.Edit;
using QPlanning.Business.UseCases.Medewerkers.Edit.Dto.Command;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class EditKlantMedewerkerUseCaseUnitTests
{
    [Fact]
    public async Task EditKlant_Handle_Should_Succeed_With_Valid_Data()
    {
        var mockService = new Mock<IKlantDomainService>();
        mockService.Setup(s => s.EditKlant(It.IsAny<EditKlantCommand>()))
            .ReturnsAsync(new BaseResponse("", true));
        var useCase = new EditKlantUseCase(mockService.Object);
        var command = new EditKlantCommand
        {
            Id = 1,
            Naam = "TestKlant"
        };

        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task EditKlant_Handle_Should_Fail_With_Empty_Naam()
    {
        var mockService = new Mock<IKlantDomainService>();
        mockService.Setup(s => s.EditKlant(It.IsAny<EditKlantCommand>()))
            .ReturnsAsync(new BaseResponse("", false));
        var useCase = new EditKlantUseCase(mockService.Object);
        var command = new EditKlantCommand
        {
            Id = 1,
            Naam = ""
        };

        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task EditMedewerker_Handle_Should_Succeed_With_Valid_Data()
    {
        var mockService = new Mock<IMedewerkerService>();
        mockService.Setup(s => s.EditMewerker(It.IsAny<EditMedewerkerCommand>()))
            .ReturnsAsync(new BaseResponse("", true));
        var useCase = new EditMedewerkerUseCase(mockService.Object);
        var command = new EditMedewerkerCommand
        {
            Id = 1,
            Voornaam = "Test",
            Achternaam = "Medewerker",
            Email = "test@example.com",
            TeamId = 1,
            IsActief = true
        };

        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task EditMedewerker_Handle_Should_Fail_With_Invalid_Id()
    {
        var mockService = new Mock<IMedewerkerService>();
        mockService.Setup(s => s.EditMewerker(It.IsAny<EditMedewerkerCommand>()))
            .ReturnsAsync(new BaseResponse("", false));
        var useCase = new EditMedewerkerUseCase(mockService.Object);
        var command = new EditMedewerkerCommand
        {
            Id = 0,
            Voornaam = "Test",
            Achternaam = "Medewerker",
            Email = "test@example.com",
            TeamId = 1,
            IsActief = true
        };

        var result = await useCase.Handle(command, CancellationToken.None);

        Assert.False(result.Success);
    }

}