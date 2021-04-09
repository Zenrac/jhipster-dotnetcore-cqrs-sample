using MediatR;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using Jhipster.Domain;
using Jhipster.Security;
using Jhipster.Domain.Services.Interfaces;
using Jhipster.Dto;
using Jhipster.Web.Extensions;
using Jhipster.Web.Filters;
using Jhipster.Web.Rest.Problems;
using Jhipster.Web.Rest.Utilities;
using Jhipster.Crosscutting.Constants;
using Jhipster.Crosscutting.Exceptions;
using Jhipster.Application.Queries;
using Jhipster.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Jhipster.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _log;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost("users")]
        [ValidateModel]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserCreateCommand command)
        {
            _log.LogDebug($"REST request to save User : {command}");
            if (command.Id != null)
                throw new BadRequestAlertException("A new user cannot already have an ID", "userManagement",
                    "idexists");

            var newUser = await this._mediator.Send(command);

            return CreatedAtAction(nameof(GetUser), new { login = newUser.Login }, newUser)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert("userManagement.created", newUser.Login));
        }

        [HttpPut("users")]
        [ValidateModel]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateCommand command)
        {
            _log.LogDebug($"REST request to update User : {command}");

            var updatedUser = await this._mediator.Send(command);
            return ActionResultUtil.WrapOrNotFound(updatedUser)
                .WithHeaders(HeaderUtil.CreateAlert("userManagement.updated", command.Login));
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Users");
            (var headers, var userDtos) = await this._mediator.Send(new UserGetAllQuery { page = pageable });
            return Ok(userDtos).WithHeaders(headers);
        }

        [HttpGet("users/authorities")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<ActionResult<IEnumerable<string>>> GetAuthorities()
        {
            return Ok(await this._mediator.Send(new UserGetAuthoritiesQuery()));
        }

        [HttpGet("users/{login}")]
        public async Task<IActionResult> GetUser([FromRoute] UserGetQuery query)
        {
            _log.LogDebug($"REST request to get User : {query.Login}");
            var userDto = await this._mediator.Send(query);
            return ActionResultUtil.WrapOrNotFound(userDto);
        }

        [HttpDelete("users/{login}")]
        [Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteUser([FromRoute] UserDeleteCommand command)
        {
            _log.LogDebug($"REST request to delete User : {command.Login}");
            await this._mediator.Send(command);
            return Ok().WithHeaders(HeaderUtil.CreateEntityDeletionAlert("userManagement.deleted", command.Login));
        }
    }
}
