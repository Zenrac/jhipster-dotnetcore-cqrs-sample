
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class EmployeeDeleteCommandHandler : IRequestHandler<EmployeeDeleteCommand, Unit>
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeDeleteCommandHandler(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(EmployeeDeleteCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.DeleteByIdAsync(request.Id);
            await _employeeRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
