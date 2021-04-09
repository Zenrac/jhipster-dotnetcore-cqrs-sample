
using Jhipster.Domain;
using Jhipster.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class DepartmentGetAllQuery : IRequest<Page<DepartmentDto>>
    {
        public IPageable page { get; set; }
    }
}
