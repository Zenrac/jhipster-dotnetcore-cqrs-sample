
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class JobHistoryUpdateCommandHandler : IRequestHandler<JobHistoryUpdateCommand, JobHistory>
    {
        private IJobHistoryRepository _jobHistoryRepository;
        private readonly IMapper _mapper;

        public JobHistoryUpdateCommandHandler(
            IMapper mapper,
            IJobHistoryRepository jobHistoryRepository)
        {
            _mapper = mapper;
            _jobHistoryRepository = jobHistoryRepository;
        }

        public async Task<JobHistory> Handle(JobHistoryUpdateCommand jobHistoryDto, CancellationToken cancellationToken)
        {
            JobHistory jobHistory = _mapper.Map<JobHistory>(jobHistoryDto);
            var entity = await _jobHistoryRepository.CreateOrUpdateAsync(jobHistory);
            await _jobHistoryRepository.SaveChangesAsync();
            return entity;
        }
    }
}
