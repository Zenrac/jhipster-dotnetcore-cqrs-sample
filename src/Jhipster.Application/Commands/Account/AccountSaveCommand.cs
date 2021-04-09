using MediatR;
using Jhipster.Dto;
using System.Security.Claims;

namespace Jhipster.Application.Commands
{
    public class AccountSaveCommand : UserDto, IRequest<Unit>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
