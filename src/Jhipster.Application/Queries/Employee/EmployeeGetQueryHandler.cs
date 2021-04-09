
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
    public class EmployeeGetQueryHandler : IRequestHandler<EmployeeGetQuery, EmployeeDto>
    {
        private IReadOnlyEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeGetQueryHandler(
            IMapper mapper,
            IReadOnlyEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> Handle(EmployeeGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _employeeRepository.QueryHelper()
                .Include(employee => employee.Manager)
                .Include(employee => employee.Department)
                .GetOneAsync(employee => employee.Id == request.Id);
            return _mapper.Map<EmployeeDto>(entity);
        }
    }
}
