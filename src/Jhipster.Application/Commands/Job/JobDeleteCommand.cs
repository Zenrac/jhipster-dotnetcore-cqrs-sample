using Jhipster.Domain;
using MediatR;

namespace Jhipster.Application.Commands
{
    public class JobDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
