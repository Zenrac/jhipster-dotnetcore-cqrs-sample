
using Jhipster.Domain;
using Jhipster.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class EmployeeGetAllQuery : IRequest<Page<EmployeeDto>>
    {
        public IPageable page { get; set; }
    }
}
