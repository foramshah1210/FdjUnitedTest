using System;
using System.Threading;
using System.Threading.Tasks;
using FdjUnited.Api.Contracts.Commands.Exchange;
using FdjUnited.Api.Contracts.DTO.Exchange;
using FdjUnited.Common.Handlers;
using FdjUnited.Common.Results;
using Microsoft.Extensions.Logging;
using FdjUnited.Api.Infrastructure.Services;

namespace FdjUnited.Api.Exchange.Application.CommandHandlers
{
    public class GenerateExchangeCommandHandler : BaseHandler<GenerateExchangeCommand, GenerateExchangeResponse>
    {
        private readonly ILogger<GenerateExchangeCommandHandler> _logger;
        private readonly IExchangerateService _exchangerateService;

        public GenerateExchangeCommandHandler(
            ILogger<GenerateExchangeCommandHandler> logger,
            IExchangerateService exchangerateService)
        {
            _logger = logger;
            _exchangerateService = exchangerateService;
        }

        protected override async Task<FdjUnitedActionResult<GenerateExchangeResponse>> HandleCommandAsync(
            GenerateExchangeCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GenerateExchangeCommandHandler)}.{nameof(HandleCommandAsync)} Exchange request started");

            try
            {
                var exchangeRates = await _exchangerateService.GetExchangeRatesAsync();
                
                if (!exchangeRates.ConversionRates.ContainsKey(request.Payload.InputCurrency))
                {
                    _logger.LogWarning($"Input currency {request.Payload.InputCurrency} not found in exchange rates");
                    return FdjUnitedBadRequest("INVALID_INPUT_CURRENCY", $"Input currency {request.Payload.InputCurrency} not found in exchange rates");
                }

                if (!exchangeRates.ConversionRates.ContainsKey(request.Payload.OutputCurrency))
                {
                    _logger.LogWarning($"Output currency {request.Payload.OutputCurrency} not found in exchange rates");
                    return FdjUnitedBadRequest("INVALID_OUTPUT_CURRENCY", $"Output currency {request.Payload.OutputCurrency} not found  in exchange rates");
                }
                
                var inputRate = exchangeRates.ConversionRates[request.Payload.InputCurrency];
                var outputRate = exchangeRates.ConversionRates[request.Payload.OutputCurrency];
                
                var convertedAmount = Math.Round(request.Payload.Amount * (outputRate / inputRate), 2);

                var response = new GenerateExchangeResponse
                {
                    InputCurrency = request.Payload.InputCurrency,
                    OutputCurrency = request.Payload.OutputCurrency,
                    Amount = request.Payload.Amount,
                    Value = convertedAmount
                };
                
                _logger.LogInformation($"{nameof(GenerateExchangeCommandHandler)}.{nameof(HandleCommandAsync)},  Exchange request completed");
                
                return FdjUnitedOk(response);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing exchange request");
                return FdjUnitedInternalServerError("EXCHANGE_ERROR", "An error occurred while processing the exchange request");
            }
        }
    }
}
