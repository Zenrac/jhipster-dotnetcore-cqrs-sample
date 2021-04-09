
using Jhipster.Domain;
using Jhipster.Dto;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class RegionGetQuery : IRequest<RegionDto>
    {
        public long Id { get; set; }
    }
}
