namespace FdjUnited.Api.Infrastructure.Services;
using System.Net.Http;
using System.Threading.Tasks;
using FdjUnited.Api.Infrastructure.Services.Models;

public interface IExchangerateService
{
    Task<ExchangeRateResponse> GetExchangeRatesAsync();
}
