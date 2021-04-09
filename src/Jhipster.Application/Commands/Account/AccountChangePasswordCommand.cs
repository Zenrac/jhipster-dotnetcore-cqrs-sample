using MediatR;
using Jhipster.Dto;

namespace Jhipster.Application.Commands
{
    public class AccountChangePasswordCommand : PasswordChangeDto, IRequest<Unit>
    {
    }
}
