
using Jhipster.Domain;
using Jhipster.Dto;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class DepartmentGetQuery : IRequest<DepartmentDto>
    {
        public long Id { get; set; }
    }
}
