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
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserUpdateCommandHandler(UserManager<User> userManager, IUserService userService,
            IMapper mapper)
        {
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<User> Handle(UserUpdateCommand userDto, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingUser != null && !existingUser.Id.Equals(userDto.Id)) throw new EmailAlreadyUsedException();
            existingUser = await _userManager.FindByNameAsync(userDto.Login);
            if (existingUser != null && !existingUser.Id.Equals(userDto.Id)) throw new LoginAlreadyUsedException();

            return await _userService.UpdateUser(_mapper.Map<User>(userDto));
        }
    }
}
