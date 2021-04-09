
using Jhipster.Domain;
using Jhipster.Dto;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class LocationGetQuery : IRequest<LocationDto>
    {
        public long Id { get; set; }
    }
}
