
using Jhipster.Domain;
using Jhipster.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class LocationGetAllQuery : IRequest<Page<LocationDto>>
    {
        public IPageable page { get; set; }
    }
}
