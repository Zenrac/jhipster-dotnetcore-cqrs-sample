
using Jhipster.Domain;
using Jhipster.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class PieceOfWorkGetAllQuery : IRequest<Page<PieceOfWorkDto>>
    {
        public IPageable page { get; set; }
    }
}
