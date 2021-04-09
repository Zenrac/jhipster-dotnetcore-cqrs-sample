using Jhipster.Domain;
using MediatR;

namespace Jhipster.Application.Commands
{
    public class CountryDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
