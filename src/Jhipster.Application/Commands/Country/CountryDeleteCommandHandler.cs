
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class CountryDeleteCommandHandler : IRequestHandler<CountryDeleteCommand, Unit>
    {
        private ICountryRepository _countryRepository;

        public CountryDeleteCommandHandler(
            ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Unit> Handle(CountryDeleteCommand request, CancellationToken cancellationToken)
        {
            await _countryRepository.DeleteByIdAsync(request.Id);
            await _countryRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
