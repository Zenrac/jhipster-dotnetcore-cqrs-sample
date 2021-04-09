
using Jhipster.Domain;
using Jhipster.Dto;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class CountryGetQuery : IRequest<CountryDto>
    {
        public long Id { get; set; }
    }
}
