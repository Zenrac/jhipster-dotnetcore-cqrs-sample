
using MediatR;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using Jhipster.Domain;
using Jhipster.Crosscutting.Exceptions;
using Jhipster.Dto;
using Jhipster.Web.Extensions;
using Jhipster.Web.Filters;
using Jhipster.Web.Rest.Utilities;
using Jhipster.Application.Queries;
using Jhipster.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Jhipster.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private const string EntityName = "location";
        private readonly ILogger<LocationController> _log;
        private readonly IMediator _mediator;

        public LocationController(ILogger<LocationController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost("locations")]
        [ValidateModel]
        public async Task<ActionResult<LocationDto>> CreateLocation([FromBody] LocationCreateCommand command)
        {
            _log.LogDebug($"REST request to save Location : {command}");
            if (command.Id != 0)
                throw new BadRequestAlertException("A new location cannot already have an ID", EntityName, "idexists");

            var location = await this._mediator.Send(command);
            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, location)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, location.Id.ToString()));
        }

        [HttpPut("locations")]
        [ValidateModel]
        public async Task<IActionResult> UpdateLocation([FromBody] LocationUpdateCommand command)
        {
            _log.LogDebug($"REST request to update Location : {command}");
            if (command.Id == 0)
                throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");

            var location = await this._mediator.Send(command);
            return Ok(location)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, location.Id.ToString()));
        }

        [HttpGet("locations")]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAllLocations(IPageable page)
        {
            _log.LogDebug("REST request to get a page of Locations");
            var result = await this._mediator.Send(new LocationGetAllQuery { page = page });
            return Ok(((IPage<LocationDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("locations/{id}")]
        public async Task<IActionResult> GetLocation([FromRoute] LocationGetQuery query)
        {
            _log.LogDebug($"REST request to get Location : {query.Id}");
            var result = await this._mediator.Send(query);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("locations/{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] LocationDeleteCommand command)
        {
            _log.LogDebug($"REST request to delete Location : {command.Id}");
            await this._mediator.Send(command);
            return Ok().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, command.Id.ToString()));
        }
    }
}
