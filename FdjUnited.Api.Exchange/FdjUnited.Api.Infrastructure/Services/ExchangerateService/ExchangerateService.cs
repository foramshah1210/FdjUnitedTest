using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using FdjUnited.Api.Infrastructure.Services.Models;

namespace FdjUnited.Api.Infrastructure.Services
{
    public class ExchangerateService : IExchangerateService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ExchangerateServiceApiEndpoint = "https://v6.exchangerate-api.com/v6/744d0d91f44398b4795cd7da/latest/USD";
      
        public ExchangerateService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ExchangeRateResponse> GetExchangeRatesAsync()
        {
            try
            {
                using var httpClient = _clientFactory.CreateClient();
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                var response = await httpClient.GetAsync(ExchangerateServiceApiEndpoint);
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("Exchange rate Service Api returned empty response");
                }

                var jsonContent = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(jsonContent))
                {
                    throw new InvalidOperationException("Exchange rate Service Api returned empty response");
                }

                var exchangeRateResponse = JsonSerializer.Deserialize<ExchangeRateResponse>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (exchangeRateResponse == null)
                {
                    throw new InvalidOperationException("Failed to deserialize exchange rate response");
                }

                if (exchangeRateResponse.Result != "success")
                {
                    throw new InvalidOperationException($"Exchangerate Service API returned error: {exchangeRateResponse.Result}");
                }
                
                return exchangeRateResponse;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (TaskCanceledException ex)
            {
                throw new TimeoutException("Exchange rate API request timed out", ex);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to parse exchange rate response", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
