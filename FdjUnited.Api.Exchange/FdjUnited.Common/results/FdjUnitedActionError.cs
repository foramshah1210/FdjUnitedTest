namespace FdjAction.Common.Results
{
    public class FdjUnitedActionError
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public FdjUnitedActionError(string code, string message = "")
        {
            Code = code;

            if (!string.IsNullOrWhiteSpace(message))
            {
                Message = message;
            }
        }

        public FdjUnitedActionError((string code, string message) errorTuple)
        {
            var (code, message) = errorTuple;
            Code = code;

            if (!string.IsNullOrWhiteSpace(message))
            {
                Message = message;
            }
        }
    }
}