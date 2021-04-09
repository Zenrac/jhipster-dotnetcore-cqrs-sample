
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
    public class EmployeeGetAllQueryHandler : IRequestHandler<EmployeeGetAllQuery, Page<EmployeeDto>>
    {
        private IReadOnlyEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeGetAllQueryHandler(
            IMapper mapper,
            IReadOnlyEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<Page<EmployeeDto>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _employeeRepository.QueryHelper()
                .Include(employee => employee.Manager)
                .Include(employee => employee.Department)
                .GetPageAsync(request.page);
            return new Page<EmployeeDto>(page.Content.Select(entity => _mapper.Map<EmployeeDto>(entity)).ToList(), request.page, page.TotalElements);
        }
    }
}
