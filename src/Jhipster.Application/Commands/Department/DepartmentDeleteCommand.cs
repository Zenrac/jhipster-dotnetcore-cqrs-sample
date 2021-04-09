using Jhipster.Domain;
using MediatR;

namespace Jhipster.Application.Commands
{
    public class DepartmentDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
