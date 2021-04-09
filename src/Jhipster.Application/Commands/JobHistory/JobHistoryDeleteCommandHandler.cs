
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class JobHistoryDeleteCommandHandler : IRequestHandler<JobHistoryDeleteCommand, Unit>
    {
        private IJobHistoryRepository _jobHistoryRepository;

        public JobHistoryDeleteCommandHandler(
            IJobHistoryRepository jobHistoryRepository)
        {
            _jobHistoryRepository = jobHistoryRepository;
        }

        public async Task<Unit> Handle(JobHistoryDeleteCommand request, CancellationToken cancellationToken)
        {
            await _jobHistoryRepository.DeleteByIdAsync(request.Id);
            await _jobHistoryRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
