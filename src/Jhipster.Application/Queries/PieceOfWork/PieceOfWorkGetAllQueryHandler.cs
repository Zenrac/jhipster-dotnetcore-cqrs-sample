
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Dto;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace Jhipster.Application.Queries
{
    public class PieceOfWorkGetAllQueryHandler : IRequestHandler<PieceOfWorkGetAllQuery, Page<PieceOfWorkDto>>
    {
        private IReadOnlyPieceOfWorkRepository _pieceOfWorkRepository;
        private readonly IMapper _mapper;

        public PieceOfWorkGetAllQueryHandler(
            IMapper mapper,
            IReadOnlyPieceOfWorkRepository pieceOfWorkRepository)
        {
            _mapper = mapper;
            _pieceOfWorkRepository = pieceOfWorkRepository;
        }

        public async Task<Page<PieceOfWorkDto>> Handle(PieceOfWorkGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _pieceOfWorkRepository.QueryHelper()
                .GetPageAsync(request.page);
            return new Page<PieceOfWorkDto>(page.Content.Select(entity => _mapper.Map<PieceOfWorkDto>(entity)).ToList(), request.page, page.TotalElements);
        }
    }
}
