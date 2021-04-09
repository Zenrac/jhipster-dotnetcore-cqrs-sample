
using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Jhipster.Dto;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Queries
{
    public class PieceOfWorkGetQueryHandler : IRequestHandler<PieceOfWorkGetQuery, PieceOfWorkDto>
    {
        private IReadOnlyPieceOfWorkRepository _pieceOfWorkRepository;
        private readonly IMapper _mapper;

        public PieceOfWorkGetQueryHandler(
            IMapper mapper,
            IReadOnlyPieceOfWorkRepository pieceOfWorkRepository)
        {
            _mapper = mapper;
            _pieceOfWorkRepository = pieceOfWorkRepository;
        }

        public async Task<PieceOfWorkDto> Handle(PieceOfWorkGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _pieceOfWorkRepository.QueryHelper()
                .GetOneAsync(pieceOfWork => pieceOfWork.Id == request.Id);
            return _mapper.Map<PieceOfWorkDto>(entity);
        }
    }
}
