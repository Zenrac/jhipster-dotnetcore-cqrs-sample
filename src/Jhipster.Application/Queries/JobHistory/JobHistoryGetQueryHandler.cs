
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Dto;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Queries
{
    public class JobHistoryGetQueryHandler : IRequestHandler<JobHistoryGetQuery, JobHistoryDto>
    {
        private IReadOnlyJobHistoryRepository _jobHistoryRepository;
        private readonly IMapper _mapper;

        public JobHistoryGetQueryHandler(
            IMapper mapper,
            IReadOnlyJobHistoryRepository jobHistoryRepository)
        {
            _mapper = mapper;
            _jobHistoryRepository = jobHistoryRepository;
        }

        public async Task<JobHistoryDto> Handle(JobHistoryGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobHistoryRepository.QueryHelper()
                .Include(jobHistory => jobHistory.Job)
                .Include(jobHistory => jobHistory.Department)
                .Include(jobHistory => jobHistory.Employee)
                .GetOneAsync(jobHistory => jobHistory.Id == request.Id);
            return _mapper.Map<JobHistoryDto>(entity);
        }
    }
}
