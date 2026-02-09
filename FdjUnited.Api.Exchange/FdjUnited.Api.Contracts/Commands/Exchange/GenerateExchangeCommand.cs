using MediatR;
using FdjUnited.Api.Contracts.DTO.Exchange;
using FdjUnited.Common.Results;

namespace FdjUnited.Api.Contracts.Commands.Exchange;
public class GenerateExchangeCommand : IRequest<FdjUnitedActionResult<GenerateExchangeResponse>>
{
    public GenerateExchangeRequest Payload { get; set; }
    
    public GenerateExchangeCommand(GenerateExchangeRequest  payload)
    {
        Payload = payload;
    }
}


    
