
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
    public class JobController : ControllerBase
    {
        private const string EntityName = "job";
        private readonly ILogger<JobController> _log;
        private readonly IMediator _mediator;

        public JobController(ILogger<JobController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost("jobs")]
        [ValidateModel]
        public async Task<ActionResult<JobDto>> CreateJob([FromBody] JobCreateCommand command)
        {
            _log.LogDebug($"REST request to save Job : {command}");
            if (command.Id != 0)
                throw new BadRequestAlertException("A new job cannot already have an ID", EntityName, "idexists");

            var job = await this._mediator.Send(command);
            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, job.Id.ToString()));
        }

        [HttpPut("jobs")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJob([FromBody] JobUpdateCommand command)
        {
            _log.LogDebug($"REST request to update Job : {command}");
            if (command.Id == 0)
                throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");

            var job = await this._mediator.Send(command);
            return Ok(job)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, job.Id.ToString()));
        }

        [HttpGet("jobs")]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetAllJobs(IPageable page)
        {
            _log.LogDebug("REST request to get a page of Jobs");
            var result = await this._mediator.Send(new JobGetAllQuery { page = page });
            return Ok(((IPage<JobDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("jobs/{id}")]
        public async Task<IActionResult> GetJob([FromRoute] JobGetQuery query)
        {
            _log.LogDebug($"REST request to get Job : {query.Id}");
            var result = await this._mediator.Send(query);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("jobs/{id}")]
        public async Task<IActionResult> DeleteJob([FromRoute] JobDeleteCommand command)
        {
            _log.LogDebug($"REST request to delete Job : {command.Id}");
            await this._mediator.Send(command);
            return Ok().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, command.Id.ToString()));
        }
    }
}
