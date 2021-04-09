
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class DepartmentCreateCommandHandler : IRequestHandler<DepartmentCreateCommand, Department>
    {
        private IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentCreateCommandHandler(
            IMapper mapper,
            IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> Handle(DepartmentCreateCommand departmentDto, CancellationToken cancellationToken)
        {
            Department department = _mapper.Map<Department>(departmentDto);
            var entity = await _departmentRepository.CreateOrUpdateAsync(department);
            await _departmentRepository.SaveChangesAsync();
            return entity;
        }
    }
}
