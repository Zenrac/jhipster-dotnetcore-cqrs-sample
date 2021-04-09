
using Jhipster.Domain;
using Jhipster.Dto;
using MediatR;

namespace Jhipster.Application.Queries
{
    public class EmployeeGetQuery : IRequest<EmployeeDto>
    {
        public long Id { get; set; }
    }
}
