using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QPlanning.Business.Dto.Base.UseCaseResponses;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.UseCases.Authorization.Claims.Roles.Create.Dto.Command;

namespace QPlanning.Business.UseCases.Authorization.Claims.Roles.Create
{
    public class CreateRoleUseCase : IRequestHandler<CreateRoleCommand, BaseResponse>
    {
        private readonly IAuthorizationService _authorizationService;

        public CreateRoleUseCase(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<BaseResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            // Inputvalidatie
            if (string.IsNullOrWhiteSpace(request.Email))
                return new BaseResponse("Email mag niet leeg zijn.", false);

            if (!request.Email.Contains("@"))
                return new BaseResponse("Email is ongeldig.", false);

            if (string.IsNullOrWhiteSpace(request.Role))
                return new BaseResponse("Role mag niet leeg zijn.", false);

            // Service aanroepen
            var response = await _authorizationService.AddRoleToUser(request.Email, request.Role);
            return response;
        }
    }
}