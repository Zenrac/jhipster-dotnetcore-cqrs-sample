using Jhipster.Domain;
using MediatR;

namespace Jhipster.Application.Commands
{
    public class JobHistoryDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
