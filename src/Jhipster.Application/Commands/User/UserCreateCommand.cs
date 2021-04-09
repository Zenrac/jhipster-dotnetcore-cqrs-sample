using Jhipster.Domain;
using MediatR;
using Jhipster.Dto;

namespace Jhipster.Application.Commands
{
    public class UserCreateCommand : UserDto, IRequest<User>
    {
    }
}
