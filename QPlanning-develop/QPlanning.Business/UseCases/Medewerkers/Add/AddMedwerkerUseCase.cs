using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QPlanning.Business.Domain.Entities;
using QPlanning.Business.Interfaces.Base;
using QPlanning.Business.Interfaces.Services;
using QPlanning.Business.UseCases.Medewerkers.Add.Dto.Command;

namespace QPlanning.Business.UseCases.Medewerkers.Add
{
    // Concrete implementatie zodat je geen abstracte klasse aanmaakt zodat UseCaseResponseMessage gebruikt kan worden 
    public class DefaultUseCaseResponseMessage : UseCaseResponseMessage
    {
        public DefaultUseCaseResponseMessage(bool success, string message = null)
            : base(success, message)
        {
        }
    }

    public class AddMedwerkerUseCase : IRequestHandler<AddMedewerkerCommand, UseCaseResponseMessage>
    {
        private readonly IMedewerkerService _medewerkerService;

        public AddMedwerkerUseCase(IMedewerkerService medewerkerService)
        {
            _medewerkerService = medewerkerService;
        }

        public async Task<UseCaseResponseMessage> Handle(AddMedewerkerCommand request, CancellationToken cancellationToken)
        {
            // Controleer of e-mail verplicht en geldig is
            if (string.IsNullOrWhiteSpace(request.Email) || !IsValidEmail(request.Email))
                return new DefaultUseCaseResponseMessage(false, "Ongeldig of ontbrekend e-mailadres");

            var result = await _medewerkerService.AddMedewerker(request);
            return result;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
