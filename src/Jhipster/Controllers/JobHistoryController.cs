
using MediatR;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using Jhipster.Domain;
using Jhipster.Crosscutting.Enums;
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
    public class JobHistoryController : ControllerBase
    {
        private const string EntityName = "jobHistory";
        private readonly ILogger<JobHistoryController> _log;
        private readonly IMediator _mediator;

        public JobHistoryController(ILogger<JobHistoryController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost("job-histories")]
        [ValidateModel]
        public async Task<ActionResult<JobHistoryDto>> CreateJobHistory([FromBody] JobHistoryCreateCommand command)
        {
            _log.LogDebug($"REST request to save JobHistory : {command}");
            if (command.Id != 0)
                throw new BadRequestAlertException("A new jobHistory cannot already have an ID", EntityName, "idexists");

            var jobHistory = await this._mediator.Send(command);
            return CreatedAtAction(nameof(GetJobHistory), new { id = jobHistory.Id }, jobHistory)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, jobHistory.Id.ToString()));
        }

        [HttpPut("job-histories")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJobHistory([FromBody] JobHistoryUpdateCommand command)
        {
            _log.LogDebug($"REST request to update JobHistory : {command}");
            if (command.Id == 0)
                throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");

            var jobHistory = await this._mediator.Send(command);
            return Ok(jobHistory)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, jobHistory.Id.ToString()));
        }

        [HttpGet("job-histories")]
        public async Task<ActionResult<IEnumerable<JobHistoryDto>>> GetAllJobHistories(IPageable page)
        {
            _log.LogDebug("REST request to get a page of JobHistories");
            var result = await this._mediator.Send(new JobHistoryGetAllQuery { page = page });
            return Ok(((IPage<JobHistoryDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("job-histories/{id}")]
        public async Task<IActionResult> GetJobHistory([FromRoute] JobHistoryGetQuery query)
        {
            _log.LogDebug($"REST request to get JobHistory : {query.Id}");
            var result = await this._mediator.Send(query);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("job-histories/{id}")]
        public async Task<IActionResult> DeleteJobHistory([FromRoute] JobHistoryDeleteCommand command)
        {
            _log.LogDebug($"REST request to delete JobHistory : {command.Id}");
            await this._mediator.Send(command);
            return Ok().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, command.Id.ToString()));
        }
    }
}
