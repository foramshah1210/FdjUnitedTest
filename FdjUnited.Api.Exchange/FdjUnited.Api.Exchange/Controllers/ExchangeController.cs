using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FdjUnited.Api.Exchange.Controllers
{
    [Route("api/ExchangeService")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ExchangeController> _logger;

        public ExchangeController(IMediator mediator, ILogger<ExchangeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateExchange(CancellationToken cancellationToken)
        {
            //_logger.LogInformation($"{FraudConstants.ClassName}::{FraudConstants.MethodName} Start {nameof(CheckBlacklistAsync)} endpoint.",
            return Ok();
        }
    }
}