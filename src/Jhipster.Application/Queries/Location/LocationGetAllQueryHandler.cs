
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
    public class LocationGetAllQueryHandler : IRequestHandler<LocationGetAllQuery, Page<LocationDto>>
    {
        private IReadOnlyLocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationGetAllQueryHandler(
            IMapper mapper,
            IReadOnlyLocationRepository locationRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }

        public async Task<Page<LocationDto>> Handle(LocationGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _locationRepository.QueryHelper()
                .Include(location => location.Country)
                .GetPageAsync(request.page);
            return new Page<LocationDto>(page.Content.Select(entity => _mapper.Map<LocationDto>(entity)).ToList(), request.page, page.TotalElements);
        }
    }
}
