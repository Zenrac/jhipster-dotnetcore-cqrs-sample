
using Jhipster.Domain;
using Jhipster.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class JobGetAllQuery : IRequest<Page<JobDto>>
    {
        public IPageable page { get; set; }
    }
}
