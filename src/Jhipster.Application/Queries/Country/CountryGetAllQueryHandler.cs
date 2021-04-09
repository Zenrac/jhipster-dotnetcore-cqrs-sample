
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Dto;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace Jhipster.Application.Queries
{
    public class CountryGetAllQueryHandler : IRequestHandler<CountryGetAllQuery, Page<CountryDto>>
    {
        private IReadOnlyCountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryGetAllQueryHandler(
            IMapper mapper,
            IReadOnlyCountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<Page<CountryDto>> Handle(CountryGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _countryRepository.QueryHelper()
                .Include(country => country.Region)
                .GetPageAsync(request.page);
            return new Page<CountryDto>(page.Content.Select(entity => _mapper.Map<CountryDto>(entity)).ToList(), request.page, page.TotalElements);
        }
    }
}
