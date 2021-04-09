
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
    public class RegionController : ControllerBase
    {
        private const string EntityName = "region";
        private readonly ILogger<RegionController> _log;
        private readonly IMediator _mediator;

        public RegionController(ILogger<RegionController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost("regions")]
        [ValidateModel]
        public async Task<ActionResult<RegionDto>> CreateRegion([FromBody] RegionCreateCommand command)
        {
            _log.LogDebug($"REST request to save Region : {command}");
            if (command.Id != 0)
                throw new BadRequestAlertException("A new region cannot already have an ID", EntityName, "idexists");

            var region = await this._mediator.Send(command);
            return CreatedAtAction(nameof(GetRegion), new { id = region.Id }, region)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, region.Id.ToString()));
        }

        [HttpPut("regions")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion([FromBody] RegionUpdateCommand command)
        {
            _log.LogDebug($"REST request to update Region : {command}");
            if (command.Id == 0)
                throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");

            var region = await this._mediator.Send(command);
            return Ok(region)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, region.Id.ToString()));
        }

        [HttpGet("regions")]
        public async Task<ActionResult<IEnumerable<RegionDto>>> GetAllRegions(IPageable page)
        {
            _log.LogDebug("REST request to get a page of Regions");
            var result = await this._mediator.Send(new RegionGetAllQuery { page = page });
            return Ok(((IPage<RegionDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("regions/{id}")]
        public async Task<IActionResult> GetRegion([FromRoute] RegionGetQuery query)
        {
            _log.LogDebug($"REST request to get Region : {query.Id}");
            var result = await this._mediator.Send(query);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("regions/{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] RegionDeleteCommand command)
        {
            _log.LogDebug($"REST request to delete Region : {command.Id}");
            await this._mediator.Send(command);
            return Ok().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, command.Id.ToString()));
        }
    }
}
