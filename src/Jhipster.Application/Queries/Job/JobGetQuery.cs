
using Jhipster.Domain;
using Jhipster.Dto;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class JobGetQuery : IRequest<JobDto>
    {
        public long Id { get; set; }
    }
}
