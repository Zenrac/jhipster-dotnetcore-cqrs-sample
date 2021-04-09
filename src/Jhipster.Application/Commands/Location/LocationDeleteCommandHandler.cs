
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class LocationDeleteCommandHandler : IRequestHandler<LocationDeleteCommand, Unit>
    {
        private ILocationRepository _locationRepository;

        public LocationDeleteCommandHandler(
            ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Unit> Handle(LocationDeleteCommand request, CancellationToken cancellationToken)
        {
            await _locationRepository.DeleteByIdAsync(request.Id);
            await _locationRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
