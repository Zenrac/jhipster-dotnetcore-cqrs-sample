using Jhipster.Domain.Services.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Jhipster.Application.Commands
{
    public class UserJwtAuthorizeCommandHandler : IRequestHandler<UserJwtAuthorizeCommand, IPrincipal>
    {
        private readonly IAuthenticationService _authenticationService;

        public UserJwtAuthorizeCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public Task<IPrincipal> Handle(UserJwtAuthorizeCommand LoginDto, CancellationToken cancellationToken)
        {
            return _authenticationService.Authenticate(LoginDto.Username, LoginDto.Password);
        }
    }
}
