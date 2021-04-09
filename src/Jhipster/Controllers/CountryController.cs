
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
    public class CountryController : ControllerBase
    {
        private const string EntityName = "country";
        private readonly ILogger<CountryController> _log;
        private readonly IMediator _mediator;

        public CountryController(ILogger<CountryController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost("countries")]
        [ValidateModel]
        public async Task<ActionResult<CountryDto>> CreateCountry([FromBody] CountryCreateCommand command)
        {
            _log.LogDebug($"REST request to save Country : {command}");
            if (command.Id != 0)
                throw new BadRequestAlertException("A new country cannot already have an ID", EntityName, "idexists");

            var country = await this._mediator.Send(command);
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, country.Id.ToString()));
        }

        [HttpPut("countries")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryUpdateCommand command)
        {
            _log.LogDebug($"REST request to update Country : {command}");
            if (command.Id == 0)
                throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");

            var country = await this._mediator.Send(command);
            return Ok(country)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, country.Id.ToString()));
        }

        [HttpGet("countries")]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAllCountries(IPageable page)
        {
            _log.LogDebug("REST request to get a page of Countries");
            var result = await this._mediator.Send(new CountryGetAllQuery { page = page });
            return Ok(((IPage<CountryDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("countries/{id}")]
        public async Task<IActionResult> GetCountry([FromRoute] CountryGetQuery query)
        {
            _log.LogDebug($"REST request to get Country : {query.Id}");
            var result = await this._mediator.Send(query);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("countries/{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] CountryDeleteCommand command)
        {
            _log.LogDebug($"REST request to delete Country : {command.Id}");
            await this._mediator.Send(command);
            return Ok().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, command.Id.ToString()));
        }
    }
}
