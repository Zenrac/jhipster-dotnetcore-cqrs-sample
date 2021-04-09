using Jhipster.Domain;
using MediatR;

namespace Jhipster.Application.Commands
{
    public class LocationDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
