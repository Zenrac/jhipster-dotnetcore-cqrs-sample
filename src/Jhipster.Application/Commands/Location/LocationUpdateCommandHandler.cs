
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class LocationUpdateCommandHandler : IRequestHandler<LocationUpdateCommand, Location>
    {
        private ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationUpdateCommandHandler(
            IMapper mapper,
            ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(LocationUpdateCommand locationDto, CancellationToken cancellationToken)
        {
            Location location = _mapper.Map<Location>(locationDto);
            var entity = await _locationRepository.CreateOrUpdateAsync(location);
            await _locationRepository.SaveChangesAsync();
            return entity;
        }
    }
}
