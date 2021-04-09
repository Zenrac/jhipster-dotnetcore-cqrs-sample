
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class DepartmentDeleteCommandHandler : IRequestHandler<DepartmentDeleteCommand, Unit>
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentDeleteCommandHandler(
            IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Unit> Handle(DepartmentDeleteCommand request, CancellationToken cancellationToken)
        {
            await _departmentRepository.DeleteByIdAsync(request.Id);
            await _departmentRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
