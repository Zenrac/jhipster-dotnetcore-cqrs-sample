
using Jhipster.Domain;
using Jhipster.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class RegionGetAllQuery : IRequest<Page<RegionDto>>
    {
        public IPageable page { get; set; }
    }
}
