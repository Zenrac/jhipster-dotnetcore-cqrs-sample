
using Jhipster.Domain;
using MediatR;
using Jhipster.Dto;

namespace Jhipster.Application.Commands
{
    public class EmployeeUpdateCommand : EmployeeDto, IRequest<Employee>
    {
    }
}
