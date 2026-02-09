using System;
using System.Net.Http;
using System.Threading.Tasks;
using FdjUnited.Api.Infrastructure.Services;
using FdjUnited.Api.Infrastructure.Services.Models;

class Program
{
    static async Task Main(string[] args)
    {
        // Create HttpClientFactory mock
        var httpClientFactory = new TestHttpClientFactory();
        
        // Create the service
        var exchangeService = new ExchangerateService(httpClientFactory);
        
        try
        {
            Console.WriteLine("Testing ExchangerateService...");
            
            // Call the service
            var result = await exchangeService.GetExchangeRatesAsync();
            
            Console.WriteLine($"Success! Retrieved exchange rates:");
            Console.WriteLine($"Base Currency: {result.BaseCode}");
            Console.WriteLine($"Result: {result.Result}");
            Console.WriteLine($"Last Update: {result.TimeLastUpdateUtc}");
            Console.WriteLine($"Number of conversion rates: {result.ConversionRates?.Count ?? 0}");
            
            // Show a few sample rates
            if (result.ConversionRates != null)
            {
                Console.WriteLine("\nSample conversion rates:");
                var count = 0;
                foreach (var rate in result.ConversionRates)
                {
                    if (count >= 5) break;
                    Console.WriteLine($"  {rate.Key}: {rate.Value}");
                    count++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }
}

public class TestHttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient(string name)
    {
        return new HttpClient();
    }
}