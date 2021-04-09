
using Jhipster.Domain;
using Jhipster.Dto;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class PieceOfWorkGetQuery : IRequest<PieceOfWorkDto>
    {
        public long Id { get; set; }
    }
}
