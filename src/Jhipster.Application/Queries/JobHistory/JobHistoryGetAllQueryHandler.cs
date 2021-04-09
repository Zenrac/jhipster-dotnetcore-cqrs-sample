
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
    public class JobHistoryGetAllQueryHandler : IRequestHandler<JobHistoryGetAllQuery, Page<JobHistoryDto>>
    {
        private IReadOnlyJobHistoryRepository _jobHistoryRepository;
        private readonly IMapper _mapper;

        public JobHistoryGetAllQueryHandler(
            IMapper mapper,
            IReadOnlyJobHistoryRepository jobHistoryRepository)
        {
            _mapper = mapper;
            _jobHistoryRepository = jobHistoryRepository;
        }

        public async Task<Page<JobHistoryDto>> Handle(JobHistoryGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _jobHistoryRepository.QueryHelper()
                .Include(jobHistory => jobHistory.Job)
                .Include(jobHistory => jobHistory.Department)
                .Include(jobHistory => jobHistory.Employee)
                .GetPageAsync(request.page);
            return new Page<JobHistoryDto>(page.Content.Select(entity => _mapper.Map<JobHistoryDto>(entity)).ToList(), request.page, page.TotalElements);
        }
    }
}
