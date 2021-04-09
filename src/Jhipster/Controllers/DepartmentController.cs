
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
    public class DepartmentController : ControllerBase
    {
        private const string EntityName = "department";
        private readonly ILogger<DepartmentController> _log;
        private readonly IMediator _mediator;

        public DepartmentController(ILogger<DepartmentController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost("departments")]
        [ValidateModel]
        public async Task<ActionResult<DepartmentDto>> CreateDepartment([FromBody] DepartmentCreateCommand command)
        {
            _log.LogDebug($"REST request to save Department : {command}");
            if (command.Id != 0)
                throw new BadRequestAlertException("A new department cannot already have an ID", EntityName, "idexists");

            var department = await this._mediator.Send(command);
            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, department.Id.ToString()));
        }

        [HttpPut("departments")]
        [ValidateModel]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentUpdateCommand command)
        {
            _log.LogDebug($"REST request to update Department : {command}");
            if (command.Id == 0)
                throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");

            var department = await this._mediator.Send(command);
            return Ok(department)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, department.Id.ToString()));
        }

        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments(IPageable page)
        {
            _log.LogDebug("REST request to get a page of Departments");
            var result = await this._mediator.Send(new DepartmentGetAllQuery { page = page });
            return Ok(((IPage<DepartmentDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("departments/{id}")]
        public async Task<IActionResult> GetDepartment([FromRoute] DepartmentGetQuery query)
        {
            _log.LogDebug($"REST request to get Department : {query.Id}");
            var result = await this._mediator.Send(query);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("departments/{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] DepartmentDeleteCommand command)
        {
            _log.LogDebug($"REST request to delete Department : {command.Id}");
            await this._mediator.Send(command);
            return Ok().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, command.Id.ToString()));
        }
    }
}
