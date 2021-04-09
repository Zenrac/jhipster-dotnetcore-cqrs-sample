using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jhipster.Domain.Services
{
    public class JobService : IJobService
    {
        protected readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public virtual async Task<Job> Save(Job job)
        {
            await _jobRepository.CreateOrUpdateAsync(job);
            await _jobRepository.SaveChangesAsync();
            return job;
        }

        public virtual async Task<IPage<Job>> FindAll(IPageable pageable)
        {
            var page = await _jobRepository.QueryHelper()
                .Include(job => job.Chores)
                .Include(job => job.Employee)
                .GetPageAsync(pageable);
            return page;
        }

        public virtual async Task<Job> FindOne(long id)
        {
            var result = await _jobRepository.QueryHelper()
                .Include(job => job.Chores)
                .Include(job => job.Employee)
                .GetOneAsync(job => job.Id == id);
            return result;
        }

        public virtual async Task Delete(long id)
        {
            await _jobRepository.DeleteByIdAsync(id);
            await _jobRepository.SaveChangesAsync();
        }
    }
}
