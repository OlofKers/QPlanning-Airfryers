using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using QPlanning.Api.Controllers;
using QPlanning.Business.Dto.Base.UseCaseResponses;
using QPlanning.Business.Dto.Commands;
using QPlanning.Business.UseCases.Authentication.Account.Update.Dto.Command;
using QPlanning.Business.UseCases.Authorization.Claims.Roles.Create.Dto.Command;
using QPlanning.Business.UseCases.Authorization.Claims.Roles.Delete.Dto.Command;

namespace QPlanning.Api.UnitTests.Controllers
{
    public class AccountControllerUnitTests
    {
        [Fact]
        public async Task Add_Returns_Ok_When_Success()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", true)); // Succesvol resultaat

            var controller = new AccountController(mockMediator.Object);

            // Gebruik geldige waarden voor het command
            var command = new CreateUserCommand(
                "Test",           // Voornaam
                "User",           // Achternaam
                "test@email.com", // Email
                "testuser",       // UserName
                "Password123"     // Password
            );

            // Act
            var result = await controller.Add(command);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task Add_Returns_BadRequest_When_Failure()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", false));
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = await controller.Add(new CreateUserCommand("", "", "", "", ""));

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_Returns_Ok_When_Success()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", true));
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = await controller.Update(new UpdateUserCommand());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Update_Returns_BadRequest_When_Failure()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", false));
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = await controller.Update(new UpdateUserCommand());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_Returns_Ok_When_Success()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", true));
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = await controller.Delete(new DeleteUserCommand());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Delete_Returns_BadRequest_When_Failure()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", false));
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = await controller.Delete(new DeleteUserCommand());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ResetPassword_Returns_Ok_When_Success()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<ResetPasswordCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", true));
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = await controller.ResetPassword(new ResetPasswordCommand());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ResetPassword_Returns_BadRequest_When_Failure()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<ResetPasswordCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", false));
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = await controller.ResetPassword(new ResetPasswordCommand());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetAllRoles_Returns_Ok()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = controller.GetAllRoles();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddClaimRoleToUser_Returns_Ok_When_Success()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CreateRoleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", true));

            var controller = new AccountController(mockMediator.Object);

            var command = new CreateRoleCommand
            {
                Email = "test@example.com",
                Role = "Admin"
            };

            // Act
            var result = await controller.AddClaimRoleToUser(command);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public async Task AddClaimRoleToUser_Returns_BadRequest_When_Failure()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CreateRoleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", false));
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = await controller.AddClaimRoleToUser(new CreateRoleCommand());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RemoveClaimRoleToUser_Returns_Ok_When_Success()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteRoleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", true));

            var controller = new AccountController(mockMediator.Object);

            var command = new DeleteRoleCommand
            {
                Email = "test@example.com",
                Role = "Admin"
            };

            // Act
            var result = await controller.RemoveClaimRoleToUser(command);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task RemoveClaimRoleToUser_Returns_BadRequest_When_Failure()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteRoleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", false));
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = await controller.RemoveClaimRoleToUser(new DeleteRoleCommand());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Add_Returns_BadRequest_When_Invalid_Email()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            // Simuleer huidige situatie: mediator geeft altijd success = true terug
            mockMediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", true));
            var controller = new AccountController(mockMediator.Object);

            // Act
            var result = await controller.Add(new CreateUserCommand("username", "notanemail", "password", "role", "extra"));

            // Assert
            // Test faalt als de actie tóch lukt (want dat mag niet)
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Add_Returns_BadRequest_When_Empty_Username()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", true));
            var controller = new AccountController(mockMediator.Object);

            var result = await controller.Add(new CreateUserCommand("", "test@example.com", "password", "role", "extra"));

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AddClaimRoleToUser_Returns_BadRequest_When_Invalid_Role()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CreateRoleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", true));
            var controller = new AccountController(mockMediator.Object);

            var result = await controller.AddClaimRoleToUser(new CreateRoleCommand { Email = "test@example.com", Role = "" });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RemoveClaimRoleToUser_Returns_BadRequest_When_Invalid_Role()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<DeleteRoleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BaseResponse("", true));
            var controller = new AccountController(mockMediator.Object);

            var result = await controller.RemoveClaimRoleToUser(new DeleteRoleCommand { Email = "test@example.com", Role = "" });

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}