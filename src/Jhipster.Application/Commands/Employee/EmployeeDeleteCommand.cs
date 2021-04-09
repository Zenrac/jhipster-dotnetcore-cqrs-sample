using Jhipster.Domain;
using MediatR;

namespace Jhipster.Application.Commands
{
    public class EmployeeDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
