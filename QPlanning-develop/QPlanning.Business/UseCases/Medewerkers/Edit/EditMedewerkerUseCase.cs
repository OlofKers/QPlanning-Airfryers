using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QPlanning.Business.Dto.Base.UseCaseResponses;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.UseCases.Medewerkers.Edit.Dto.Command;

namespace QPlanning.Business.UseCases.Medewerkers.Edit
{
    public class EditMedewerkerUseCase : IRequestHandler<EditMedewerkerCommand, BaseResponse>
    {
        private readonly IMedewerkerService _medewerkerService;

        public EditMedewerkerUseCase(IMedewerkerService medewerkerService)
        {
            _medewerkerService = medewerkerService;
        }

        public async Task<BaseResponse> Handle(EditMedewerkerCommand request, CancellationToken cancellationToken)
        {
            // Basisvalidaties
            if (request.Id <= 0) return new BaseResponse("Invalid Id", false);
            if (string.IsNullOrWhiteSpace(request.Voornaam)) return new BaseResponse("Voornaam is required", false);
            if (string.IsNullOrWhiteSpace(request.Email) || !request.Email.Contains("@")) return new BaseResponse("Invalid email", false);
            if (request.TeamId <= 0) return new BaseResponse("Invalid TeamId", false);

            // Call service
            var result = await _medewerkerService.EditMewerker(request);
            return result;
        }
    }
}