using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QPlanning.Business.Domain.Entities;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.UseCases.Boeking.Add.Dto;
using QPlanning.Business.UseCases.Boeking.Update.Dto;

namespace QPlanning.Business.UseCases.Boeking.Update
{
    public class UpdateBoekingUseCase : IRequestHandler<UpdateBoekingCommand, BoekingResponse>
    {
        private readonly IBoekingService _boekingService;

        public UpdateBoekingUseCase(IBoekingService boekingService)
        {
            _boekingService = boekingService;
        }

        public async Task<BoekingResponse> Handle(UpdateBoekingCommand request, CancellationToken cancellationToken)
        {
            // --- VALIDATIE ---
            if (!request.MedewerkerId.HasValue || request.MedewerkerId <= 0)
                return new BoekingResponse(0, false, "Ongeldige medewerkerId");

            if (request.Uren < 0)
                return new BoekingResponse(0, false, "Uren kunnen niet negatief zijn");

            if (!request.KlantId.HasValue || request.KlantId <= 0)
                return new BoekingResponse(0, false, "Ongeldige klantId");

            if (!request.Weeknummer.HasValue || request.Weeknummer <= 0)
                return new BoekingResponse(0, false, "Ongeldig weeknummer");

            if (!request.Jaar.HasValue || request.Jaar <= 0)
                return new BoekingResponse(0, false, "Ongeldig jaar");

            // --- DOMAIN MODEL CREËREN ---
            var domainModelBoeking = new DomainModelBoeking
            {
                Jaar = request.Jaar,
                Boekjaar = request.Boekjaar,
                Uren = request.Uren,
                Weeknummer = request.Weeknummer,
                Datum = request.PlannedDate,
                KlantId = request.KlantId,
                OpdrachtId = request.OpdrachtId,
                MedewerkerId = (int)request.MedewerkerId,
                IndirecteUrenId = request.IndirecteUrenId
            };

            if (request.Id.HasValue)
                domainModelBoeking.Id = request.Id.Value;

            // --- SERVICE AANROEPEN ---
            var response = await _boekingService.UpdateBoeking(domainModelBoeking);
            return response;
        }
    }
}