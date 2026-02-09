using System.Threading;
using System.Threading.Tasks;
using FdjUnited.Api.Contracts.Commands.Exchange;
using FdjUnited.Api.Contracts.DTO.Exchange;
using FdjUnited.Common.Handlers;
using FdjUnited.Common.Results;
using Microsoft.Extensions.Logging;
using FdjUnited.Common.Handlers;

namespace FdjUnited.Api.Exchange.Application.CommandHandlers
{
    public class GenerateExchangeCommandHandler : BaseHandler<GenerateExchangeCommand, GenerateExchangeResponse>
    {
        private readonly ILogger<GenerateExchangeCommandHandler> _logger;

        public GenerateExchangeCommandHandler(
            ILogger<GenerateExchangeCommandHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<FdjUnitedActionResult<GenerateExchangeResponse>> HandleCommandAsync(
            GenerateExchangeCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                $"{nameof(GenerateExchangeCommandHandler)}.{nameof(HandleCommandAsync)} Exchange request started");

            var response = new GenerateExchangeResponse();
            return FdjUnitedOk(response);
        }
    }
}