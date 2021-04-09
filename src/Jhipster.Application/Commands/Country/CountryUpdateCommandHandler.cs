
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class CountryUpdateCommandHandler : IRequestHandler<CountryUpdateCommand, Country>
    {
        private ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryUpdateCommandHandler(
            IMapper mapper,
            ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<Country> Handle(CountryUpdateCommand countryDto, CancellationToken cancellationToken)
        {
            Country country = _mapper.Map<Country>(countryDto);
            var entity = await _countryRepository.CreateOrUpdateAsync(country);
            await _countryRepository.SaveChangesAsync();
            return entity;
        }
    }
}
