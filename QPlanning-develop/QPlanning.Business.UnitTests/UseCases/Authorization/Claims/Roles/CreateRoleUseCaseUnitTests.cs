using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using QPlanning.Business.Dto.Base.UseCaseResponses;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.UseCases.Authorization.Claims.Roles.Create;
using QPlanning.Business.UseCases.Authorization.Claims.Roles.Create.Dto.Command;
using QPlanning.Business.UseCases.Authorization.Claims.Roles.Delete;
using QPlanning.Business.UseCases.Authorization.Claims.Roles.Delete.Dto.Command;

public class RoleUseCaseUnitTests
{
    [Fact]
    public async Task CreateRole_Handle_Should_Succeed()
    {
        // Arrange
        var mockService = new Mock<IAuthorizationService>();
        mockService.Setup(s => s.AddRoleToUser(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new BaseResponse("", true));
        var useCase = new CreateRoleUseCase(mockService.Object);
        var command = new CreateRoleCommand { Email = "test@example.com", Role = "Admin" };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Role created", result.Message);
    }

    [Fact]
    public async Task DeleteRole_Handle_Should_Succeed()
    {
        // Arrange
        var mockService = new Mock<IAuthorizationService>();
        mockService.Setup(s => s.DeleteClaimRole(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((string email, string role) => new BaseResponse("", true, "Role deleted"));
        var useCase = new DeleteRoleUseCase(mockService.Object);
        var command = new DeleteRoleCommand { Email = "test@example.com", Role = "Admin" };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Role deleted", result.Message);
    }

    [Fact]
    public async Task CreateRole_Handle_Should_Fail_When_NotAllowed()
    {
        // Arrange
        var mockService = new Mock<IAuthorizationService>();
        mockService.Setup(s => s.AddRoleToUser(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new BaseResponse("Not allowed", false));
        var useCase = new CreateRoleUseCase(mockService.Object);
        var command = new CreateRoleCommand { Email = "test@example.com", Role = "Admin" };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Not allowed", result.Message);
    }

    [Fact]
    public async Task DeleteRole_Handle_Should_Fail_When_NotAllowed()
    {
        // Arrange
        var mockService = new Mock<IAuthorizationService>();
        mockService.Setup(s => s.DeleteClaimRole(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((string email, string role) => new BaseResponse("", false, "Not allowed"));
        var useCase = new DeleteRoleUseCase(mockService.Object);
        var command = new DeleteRoleCommand { Email = "test@example.com", Role = "Admin" };

        // Act
        var result = await useCase.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Not allowed", result.Message);
    }
}
