
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class JobCreateCommandHandler : IRequestHandler<JobCreateCommand, Job>
    {
        private IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobCreateCommandHandler(
            IMapper mapper,
            IJobRepository jobRepository)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
        }

        public async Task<Job> Handle(JobCreateCommand jobDto, CancellationToken cancellationToken)
        {
            Job job = _mapper.Map<Job>(jobDto);
            var entity = await _jobRepository.CreateOrUpdateAsync(job);
            await _jobRepository.SaveChangesAsync();
            return entity;
        }
    }
}
