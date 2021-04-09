using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jhipster.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        protected readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public virtual async Task<Employee> Save(Employee employee)
        {
            await _employeeRepository.CreateOrUpdateAsync(employee);
            await _employeeRepository.SaveChangesAsync();
            return employee;
        }

        public virtual async Task<IPage<Employee>> FindAll(IPageable pageable)
        {
            var page = await _employeeRepository.QueryHelper()
                .Include(employee => employee.Manager)
                .Include(employee => employee.Department)
                .GetPageAsync(pageable);
            return page;
        }

        public virtual async Task<Employee> FindOne(long id)
        {
            var result = await _employeeRepository.QueryHelper()
                .Include(employee => employee.Manager)
                .Include(employee => employee.Department)
                .GetOneAsync(employee => employee.Id == id);
            return result;
        }

        public virtual async Task Delete(long id)
        {
            await _employeeRepository.DeleteByIdAsync(id);
            await _employeeRepository.SaveChangesAsync();
        }
    }
}
