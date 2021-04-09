using MediatR;
using Jhipster.Dto;
using System.Security.Principal;

namespace Jhipster.Application.Commands
{
    public class UserJwtAuthorizeCommand : LoginDto, IRequest<IPrincipal>
    {
    }
}
