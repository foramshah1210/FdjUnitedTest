using System.Collections.Generic;
using FdjAction.Common.Results;

namespace FdjUnited.Common.Results
{
    public interface IFdjUnitedActionResult
    {
        int Status { get; }

        bool IsSuccess { get; }

        IList<FdjUnitedActionError> Errors { get; }
    }
}