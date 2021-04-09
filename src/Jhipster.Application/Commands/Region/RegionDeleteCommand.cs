using Jhipster.Domain;
using MediatR;

namespace Jhipster.Application.Commands
{
    public class RegionDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
