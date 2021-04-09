using Jhipster.Domain;
using MediatR;

namespace Jhipster.Application.Commands
{
    public class PieceOfWorkDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
