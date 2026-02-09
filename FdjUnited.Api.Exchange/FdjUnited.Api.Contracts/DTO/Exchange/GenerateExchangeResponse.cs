namespace FdjUnited.Api.Contracts.DTO.Exchange;

public class GenerateExchangeResponse
{
    public decimal Amount { get; set; }
    
    public string InputCurrency { get; set; }
    
    public string OutputCurrency { get; set; }
    
    public decimal Value { get; set; }
}