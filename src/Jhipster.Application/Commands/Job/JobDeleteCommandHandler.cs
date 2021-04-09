
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class JobDeleteCommandHandler : IRequestHandler<JobDeleteCommand, Unit>
    {
        private IJobRepository _jobRepository;

        public JobDeleteCommandHandler(
            IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Unit> Handle(JobDeleteCommand request, CancellationToken cancellationToken)
        {
            await _jobRepository.DeleteByIdAsync(request.Id);
            await _jobRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
