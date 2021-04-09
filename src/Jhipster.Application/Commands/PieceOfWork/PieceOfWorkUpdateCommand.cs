
using Jhipster.Domain;
using MediatR;
using Jhipster.Dto;

namespace Jhipster.Application.Commands
{
    public class PieceOfWorkUpdateCommand : PieceOfWorkDto, IRequest<PieceOfWork>
    {
    }
}
