
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
    public class PieceOfWorkController : ControllerBase
    {
        private const string EntityName = "pieceOfWork";
        private readonly ILogger<PieceOfWorkController> _log;
        private readonly IMediator _mediator;

        public PieceOfWorkController(ILogger<PieceOfWorkController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost("piece-of-works")]
        [ValidateModel]
        public async Task<ActionResult<PieceOfWorkDto>> CreatePieceOfWork([FromBody] PieceOfWorkCreateCommand command)
        {
            _log.LogDebug($"REST request to save PieceOfWork : {command}");
            if (command.Id != 0)
                throw new BadRequestAlertException("A new pieceOfWork cannot already have an ID", EntityName, "idexists");

            var pieceOfWork = await this._mediator.Send(command);
            return CreatedAtAction(nameof(GetPieceOfWork), new { id = pieceOfWork.Id }, pieceOfWork)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, pieceOfWork.Id.ToString()));
        }

        [HttpPut("piece-of-works")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePieceOfWork([FromBody] PieceOfWorkUpdateCommand command)
        {
            _log.LogDebug($"REST request to update PieceOfWork : {command}");
            if (command.Id == 0)
                throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");

            var pieceOfWork = await this._mediator.Send(command);
            return Ok(pieceOfWork)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, pieceOfWork.Id.ToString()));
        }

        [HttpGet("piece-of-works")]
        public async Task<ActionResult<IEnumerable<PieceOfWorkDto>>> GetAllPieceOfWorks(IPageable page)
        {
            _log.LogDebug("REST request to get a page of PieceOfWorks");
            var result = await this._mediator.Send(new PieceOfWorkGetAllQuery { page = page });
            return Ok(((IPage<PieceOfWorkDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("piece-of-works/{id}")]
        public async Task<IActionResult> GetPieceOfWork([FromRoute] PieceOfWorkGetQuery query)
        {
            _log.LogDebug($"REST request to get PieceOfWork : {query.Id}");
            var result = await this._mediator.Send(query);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("piece-of-works/{id}")]
        public async Task<IActionResult> DeletePieceOfWork([FromRoute] PieceOfWorkDeleteCommand command)
        {
            _log.LogDebug($"REST request to delete PieceOfWork : {command.Id}");
            await this._mediator.Send(command);
            return Ok().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, command.Id.ToString()));
        }
    }
}
