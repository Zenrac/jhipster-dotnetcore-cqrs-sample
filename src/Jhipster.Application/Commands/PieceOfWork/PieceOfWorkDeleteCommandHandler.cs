
using Jhipster.Domain;
using Jhipster.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class PieceOfWorkDeleteCommandHandler : IRequestHandler<PieceOfWorkDeleteCommand, Unit>
    {
        private IPieceOfWorkRepository _pieceOfWorkRepository;

        public PieceOfWorkDeleteCommandHandler(
            IPieceOfWorkRepository pieceOfWorkRepository)
        {
            _pieceOfWorkRepository = pieceOfWorkRepository;
        }

        public async Task<Unit> Handle(PieceOfWorkDeleteCommand request, CancellationToken cancellationToken)
        {
            await _pieceOfWorkRepository.DeleteByIdAsync(request.Id);
            await _pieceOfWorkRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
