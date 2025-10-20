using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QPlanning.Business.Domain.Entities;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.UseCases.Boeking.Add.Dto;

namespace QPlanning.Business.UseCases.Boeking.Add
{
    public class AddBoekingUseCase : IRequestHandler<AddBoekingCommand, BoekingResponse>
    {
        private readonly IBoekingService _boekingService;

        public AddBoekingUseCase(IBoekingService boekingService)
        {
            _boekingService = boekingService;
        }

        public async Task<BoekingResponse> Handle(AddBoekingCommand request, CancellationToken cancellationToken)
        {
            // --- VALIDATIE ---
            if ((request.MedewerkerIds == null || request.MedewerkerIds.Count == 0)
                && !request.MedewerkerId.HasValue)
            {
                return new BoekingResponse(0, false, "Geen medewerker opgegeven");
            }

            if (request.Uren < 0)
                return new BoekingResponse(0, false, "Uren kunnen niet negatief zijn");

            if (!request.KlantId.HasValue || request.KlantId <= 0)
                return new BoekingResponse(0, false, "Ongeldige klantId");

            if (!request.Weeknummer.HasValue || request.Weeknummer <= 0)
                return new BoekingResponse(0, false, "Ongeldige weeknummer");

            // --- DOMAIN MODEL CREËREN ---
            var domainModelBoekingen = new List<DomainModelBoeking>();

            if (request.MedewerkerIds != null && request.MedewerkerIds.Count > 0)
            {
                foreach (var medewerkerId in request.MedewerkerIds)
                {
                    CreateDomainModelBoeking(request, domainModelBoekingen, medewerkerId);
                }
            }
            else if (request.MedewerkerId.HasValue)
            {
                CreateDomainModelBoeking(request, domainModelBoekingen, request.MedewerkerId.Value);
            }

            // --- SERVICE AANROEPEN ---
            var response = await _boekingService.AddBoekingen(domainModelBoekingen);
            return response;
        }

        private static void CreateDomainModelBoeking(AddBoekingCommand request,
            List<DomainModelBoeking> domainModelBoekingen, int medewerkerId)
        {
            var domainModelBoeking = new DomainModelBoeking
            {
                Jaar = request.Jaar,
                Boekjaar = request.Boekjaar,
                Uren = request.Uren,
                Weeknummer = request.Weeknummer,
                Datum = request.PlannedDate,
                KlantId = request.KlantId,
                OpdrachtId = request.OpdrachtId,
                MedewerkerId = medewerkerId,
                IndirecteUrenId = request.IndirecteUrenId
            };
            domainModelBoekingen.Add(domainModelBoeking);
        }
    }
}
