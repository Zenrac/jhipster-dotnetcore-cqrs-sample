using Jhipster.Domain;
using MediatR;
using Jhipster.Dto;

namespace Jhipster.Application.Commands
{
    public class AccountCreateCommand : ManagedUserDto, IRequest<User>
    {
    }
}
