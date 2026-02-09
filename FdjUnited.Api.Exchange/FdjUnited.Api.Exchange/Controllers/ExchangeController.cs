using System.Threading;
using System.Threading.Tasks;
using FdjUnited.Api.Contracts.Commands.Exchange;
using FdjUnited.Api.Contracts.DTO.Exchange;
using FdjUnited.Common.Results;
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
        public async Task<FdjUnitedActionResult<GenerateExchangeResponse>> GenerateExchange([FromBody] GenerateExchangeRequest request)
        {
            //_logger.LogInformation($"{FraudConstants.ClassName}::{FraudConstants.MethodName} Start {nameof(CheckBlacklistAsync)} endpoint.",
            var command = new GenerateExchangeCommand(request);
            return await _mediator.Send(command);
        }
    }
}