
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
    public class DepartmentGetAllQueryHandler : IRequestHandler<DepartmentGetAllQuery, Page<DepartmentDto>>
    {
        private IReadOnlyDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentGetAllQueryHandler(
            IMapper mapper,
            IReadOnlyDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public async Task<Page<DepartmentDto>> Handle(DepartmentGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _departmentRepository.QueryHelper()
                .Include(department => department.Location)
                .GetPageAsync(request.page);
            return new Page<DepartmentDto>(page.Content.Select(entity => _mapper.Map<DepartmentDto>(entity)).ToList(), request.page, page.TotalElements);
        }
    }
}
