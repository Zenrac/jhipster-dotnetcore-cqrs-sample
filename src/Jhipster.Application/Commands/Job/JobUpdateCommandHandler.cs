
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class JobUpdateCommandHandler : IRequestHandler<JobUpdateCommand, Job>
    {
        private IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobUpdateCommandHandler(
            IMapper mapper,
            IJobRepository jobRepository)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
        }

        public async Task<Job> Handle(JobUpdateCommand jobDto, CancellationToken cancellationToken)
        {
            Job job = _mapper.Map<Job>(jobDto);
            var entity = await _jobRepository.CreateOrUpdateAsync(job);
            await _jobRepository.SaveChangesAsync();
            return entity;
        }
    }
}
