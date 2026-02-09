using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FdjUnited.Api.Contracts.Commands.Exchange;
using FdjUnited.Api.Contracts.DTO.Exchange;
using FdjUnited.Api.Exchange.Application.CommandHandlers;
using FdjUnited.Api.Infrastructure.Services;
using FdjUnited.Api.Infrastructure.Services.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FdjUnited.UnitTests.Api.Handlers
{
    public class GenerateExchangeCommandHandlerTests
    {
        private readonly Mock<ILogger<GenerateExchangeCommandHandler>> _mockLogger;
        private readonly Mock<IExchangerateService> _mockExchangerateService;
        private readonly GenerateExchangeCommandHandler _handler;

        public GenerateExchangeCommandHandlerTests()
        {
            _mockLogger = new Mock<ILogger<GenerateExchangeCommandHandler>>();
            _mockExchangerateService = new Mock<IExchangerateService>();
            _handler = new GenerateExchangeCommandHandler(_mockLogger.Object, _mockExchangerateService.Object);
        }

        [Fact]
        public async Task HandleCommandAsync_ValidCurrencyConversion_ReturnsSuccessResult()
        {
            // Arrange
            var request = new GenerateExchangeCommand(new GenerateExchangeRequest
            {
                Amount = 100m,
                InputCurrency = "AUD",
                OutputCurrency = "USD"
            });

            var exchangeRateResponse = new ExchangeRateResponse
            {
                Result = "success",
                BaseCode = "USD",
                ConversionRates = new Dictionary<string, decimal>
                {
                    { "USD", 1m },
                    { "AUD", 1.4241m },
                    { "INR", 90.6281m }
                }
            };

            _mockExchangerateService.Setup(x => x.GetExchangeRatesAsync()) .ReturnsAsync(exchangeRateResponse);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal("AUD", result.Data.InputCurrency);
            Assert.Equal("USD", result.Data.OutputCurrency);
            Assert.Equal(100, result.Data.Amount);
            Assert.Equal(70.22m, result.Data.Value);

            _mockExchangerateService.Verify(x => x.GetExchangeRatesAsync(), Times.Once);
        }

        [Fact]
        public async Task HandleCommandAsync_InvalidInputCurrency_ReturnsBadRequestResult()
        {
            // Arrange
            var request = new GenerateExchangeCommand(new GenerateExchangeRequest
            {
                Amount = 100m,
                InputCurrency = "INVALID",
                OutputCurrency = "USD"
            });

            var exchangeRateResponse = new ExchangeRateResponse
            {
                Result = "success",
                BaseCode = "USD",
                ConversionRates = new Dictionary<string, decimal>
                {
                    { "USD", 1.0m },
                    { "EUR", 0.85m },
                    { "GBP", 0.75m }
                }
            };

            _mockExchangerateService .Setup(x => x.GetExchangeRatesAsync()) .ReturnsAsync(exchangeRateResponse);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.Status);
            Assert.Null(result.Data);
            Assert.Single(result.Errors);
            Assert.Equal("INVALID_INPUT_CURRENCY", result.Errors.First().Code);

            _mockExchangerateService.Verify(x => x.GetExchangeRatesAsync(), Times.Once);
        }
    }
}