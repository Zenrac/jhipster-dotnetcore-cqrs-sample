
using Jhipster.Domain;
using Jhipster.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class JobHistoryGetAllQuery : IRequest<Page<JobHistoryDto>>
    {
        public IPageable page { get; set; }
    }
}
