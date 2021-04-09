using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using Jhipster.Domain;

namespace Jhipster.Domain.Services.Interfaces
{
    public interface IJobService
    {
        Task<Job> Save(Job job);

        Task<IPage<Job>> FindAll(IPageable pageable);

        Task<Job> FindOne(long id);

        Task Delete(long id);
    }
}
