namespace FdjUnited.Api.Contracts.DTO.Exchange;

public class GenerateExchangeRequest
{
    public decimal Amount { get; set; }
    
    public string InputCurrency { get; set; }
    
    public string OutputCurrency { get; set; }
}