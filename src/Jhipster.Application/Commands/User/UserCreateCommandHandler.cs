using AutoMapper;
using Jhipster.Domain;
using Jhipster.Domain.Services.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Jhipster.Crosscutting.Exceptions;

namespace Jhipster.Application.Commands
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, User>
    {
        private readonly IMailService _mailService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserCreateCommandHandler(UserManager<User> userManager, IUserService userService,
            IMapper mapper, IMailService mailService)
        {
            _userManager = userManager;
            _userService = userService;
            _mailService = mailService;
            _mapper = mapper;
        }

        public async Task<User> Handle(UserCreateCommand userDto, CancellationToken cancellationToken)
        {
            // Lowercase the user login before comparing with database
            if (await _userManager.FindByNameAsync(userDto.Login.ToLowerInvariant()) != null)
                throw new LoginAlreadyUsedException();
            if (await _userManager.FindByEmailAsync(userDto.Email.ToLowerInvariant()) != null)
                throw new EmailAlreadyUsedException();

            var newUser = await _userService.CreateUser(_mapper.Map<User>(userDto));
            await _mailService.SendCreationEmail(newUser);
            return newUser;
        }
    }
}
