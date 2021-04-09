
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
    public class EmployeeController : ControllerBase
    {
        private const string EntityName = "employee";
        private readonly ILogger<EmployeeController> _log;
        private readonly IMediator _mediator;

        public EmployeeController(ILogger<EmployeeController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost("employees")]
        [ValidateModel]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] EmployeeCreateCommand command)
        {
            _log.LogDebug($"REST request to save Employee : {command}");
            if (command.Id != 0)
                throw new BadRequestAlertException("A new employee cannot already have an ID", EntityName, "idexists");

            var employee = await this._mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, employee.Id.ToString()));
        }

        [HttpPut("employees")]
        [ValidateModel]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeUpdateCommand command)
        {
            _log.LogDebug($"REST request to update Employee : {command}");
            if (command.Id == 0)
                throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");

            var employee = await this._mediator.Send(command);
            return Ok(employee)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, employee.Id.ToString()));
        }

        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees(IPageable page)
        {
            _log.LogDebug("REST request to get a page of Employees");
            var result = await this._mediator.Send(new EmployeeGetAllQuery { page = page });
            return Ok(((IPage<EmployeeDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("employees/{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] EmployeeGetQuery query)
        {
            _log.LogDebug($"REST request to get Employee : {query.Id}");
            var result = await this._mediator.Send(query);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("employees/{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] EmployeeDeleteCommand command)
        {
            _log.LogDebug($"REST request to delete Employee : {command.Id}");
            await this._mediator.Send(command);
            return Ok().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, command.Id.ToString()));
        }
    }
}
