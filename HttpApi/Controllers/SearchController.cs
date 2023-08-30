using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditRating.Visualization.Host.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SearchController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public SearchController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get Credit Score
        /// </summary>
        /// <response code="400">If parameter is invalid</response>
        /// <response code="500">Internal error</response>
        /// <response code="200">Success</response>
        [HttpPost("GetCreditScore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromBody] GetCreditScoreQuery request)
        {
            var creditScore =
                await _mediator.Send(request);

            return StatusCode(StatusCodes.Status200OK,
                new HttpBaseResponse<CalculateCreditRatingResponseDto>(creditScore));
        }


        /// <summary>
        /// Update CreditRating
        /// </summary>
        /// <returns>Result of update procedure</returns>
        /// <response code="400">If parameter is invalid</response>
        /// <response code="500">Internal error</response>
        /// <response code="200">Success</response>
        [HttpPut("UpdateCreditScore")]
        [ProducesResponseType(typeof(ICommonError), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Update([FromBody] UpdateCreditScoreCommand command)
        {
            var result = await _mediator.Send(command);
            _logger.Information($"Updated CreditScore by {nameof(command.CreditscoreProductId)}: {command.CreditscoreProductId.ToString()}");

            return StatusCode(StatusCodes.Status200OK, new HttpBaseResponse<UpdateCreditScoreResultDto>(result));
        }

    }
}
