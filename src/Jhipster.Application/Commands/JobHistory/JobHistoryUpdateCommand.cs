
using Jhipster.Domain;
using MediatR;
using Jhipster.Dto;

namespace Jhipster.Application.Commands
{
    public class JobHistoryUpdateCommand : JobHistoryDto, IRequest<JobHistory>
    {
    }
}
