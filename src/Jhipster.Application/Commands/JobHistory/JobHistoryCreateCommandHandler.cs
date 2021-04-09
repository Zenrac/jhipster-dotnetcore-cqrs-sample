
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class JobHistoryCreateCommandHandler : IRequestHandler<JobHistoryCreateCommand, JobHistory>
    {
        private IJobHistoryRepository _jobHistoryRepository;
        private readonly IMapper _mapper;

        public JobHistoryCreateCommandHandler(
            IMapper mapper,
            IJobHistoryRepository jobHistoryRepository)
        {
            _mapper = mapper;
            _jobHistoryRepository = jobHistoryRepository;
        }

        public async Task<JobHistory> Handle(JobHistoryCreateCommand jobHistoryDto, CancellationToken cancellationToken)
        {
            JobHistory jobHistory = _mapper.Map<JobHistory>(jobHistoryDto);
            var entity = await _jobHistoryRepository.CreateOrUpdateAsync(jobHistory);
            await _jobHistoryRepository.SaveChangesAsync();
            return entity;
        }
    }
}
