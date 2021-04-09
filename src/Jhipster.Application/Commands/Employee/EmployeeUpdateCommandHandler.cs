
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class EmployeeUpdateCommandHandler : IRequestHandler<EmployeeUpdateCommand, Employee>
    {
        private IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeUpdateCommandHandler(
            IMapper mapper,
            IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(EmployeeUpdateCommand employeeDto, CancellationToken cancellationToken)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            var entity = await _employeeRepository.CreateOrUpdateAsync(employee);
            await _employeeRepository.SaveChangesAsync();
            return entity;
        }
    }
}
