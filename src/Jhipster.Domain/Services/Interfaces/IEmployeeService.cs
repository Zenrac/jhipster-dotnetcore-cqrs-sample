using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using Jhipster.Domain;

namespace Jhipster.Domain.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> Save(Employee employee);

        Task<IPage<Employee>> FindAll(IPageable pageable);

        Task<Employee> FindOne(long id);

        Task Delete(long id);
    }
}
