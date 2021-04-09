
using Jhipster.Domain;
using Jhipster.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class CountryGetAllQuery : IRequest<Page<CountryDto>>
    {
        public IPageable page { get; set; }
    }
}
