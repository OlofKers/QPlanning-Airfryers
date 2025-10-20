using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QPlanning.Business.Dto.Base.UseCaseResponses;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.UseCases.Authorization.Claims.Roles.Delete.Dto.Command;

namespace QPlanning.Business.UseCases.Authorization.Claims.Roles.Delete
{
    public class DeleteRoleUseCase : IRequestHandler<DeleteRoleCommand, BaseResponse>
    {
        private readonly IAuthorizationService _authorizationService;

        public DeleteRoleUseCase(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<BaseResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            // Inputvalidatie
            if (string.IsNullOrWhiteSpace(request.Email))
                return new BaseResponse("Email mag niet leeg zijn.", false);

            if (!request.Email.Contains("@"))
                return new BaseResponse("Email is ongeldig.", false);

            if (string.IsNullOrWhiteSpace(request.Role))
                return new BaseResponse("Role mag niet leeg zijn.", false);

            // Service aanroepen
            var response = await _authorizationService.DeleteClaimRole(request.Email, request.Role);
            return response;
        }
    }
}