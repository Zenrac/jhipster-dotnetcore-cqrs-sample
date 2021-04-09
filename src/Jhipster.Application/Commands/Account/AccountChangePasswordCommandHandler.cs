using Jhipster.Domain.Services.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jhipster.Application.Commands
{
    public class AccountChangePasswordCommandHandler : IRequestHandler<AccountChangePasswordCommand, Unit>
    {
        private readonly IUserService _userService;

        public AccountChangePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Unit> Handle(AccountChangePasswordCommand passwordChangeDto, CancellationToken cancellationToken)
        {
            await _userService.ChangePassword(passwordChangeDto.CurrentPassword, passwordChangeDto.NewPassword);
            return Unit.Value;
        }
    }
}
