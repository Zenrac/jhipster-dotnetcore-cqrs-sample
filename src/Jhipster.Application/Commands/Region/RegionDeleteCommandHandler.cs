
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class RegionDeleteCommandHandler : IRequestHandler<RegionDeleteCommand, Unit>
    {
        private IRegionRepository _regionRepository;

        public RegionDeleteCommandHandler(
            IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<Unit> Handle(RegionDeleteCommand request, CancellationToken cancellationToken)
        {
            await _regionRepository.DeleteByIdAsync(request.Id);
            await _regionRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
