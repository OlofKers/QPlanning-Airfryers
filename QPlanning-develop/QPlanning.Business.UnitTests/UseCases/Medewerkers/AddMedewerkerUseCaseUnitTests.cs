using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using QPlanning.Business.Dto.Base.UseCaseResponses;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.UseCases.Medewerkers.Add;
using QPlanning.Business.UseCases.Medewerkers.Add.Dto.Command;

public class AddMedewerkerUseCaseUnitTests
{
    [Theory]
    [InlineData("plainaddress")]
    [InlineData("missingatsign.com")]
    [InlineData("missingdomain@")]
    [InlineData("@missinglocal.com")]
    [InlineData("notanemail")]
    public async Task Handle_Should_Fail_With_Invalid_Email_Format(string invalidEmail)
    {
        // Arrange
        var mockService = new Mock<IMedewerkerService>();
        // Simuleer de echte situatie: de service geeft altijd success = true terug, want er is geen validatie
        mockService.Setup(s => s.AddMedewerker(It.IsAny<AddMedewerkerCommand>()))
            .ReturnsAsync(new BaseResponse("", true));
        var useCase = new AddMedwerkerUseCase(mockService.Object);
        var command = new AddMedewerkerCommand
        {
            Voornaam = "Test",
            Achternaam = "Medewerker",
            Email = invalidEmail,
            TeamId = 1
        };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        // Deze assert zal nu falen, want er is geen validatie en success is altijd true
        Assert.False(result.Success); // Test faalt nu terecht!
    }
}