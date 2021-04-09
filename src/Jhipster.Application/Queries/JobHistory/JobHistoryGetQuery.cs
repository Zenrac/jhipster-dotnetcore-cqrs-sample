
using Jhipster.Domain;
using Jhipster.Dto;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class JobHistoryGetQuery : IRequest<JobHistoryDto>
    {
        public long Id { get; set; }
    }
}
