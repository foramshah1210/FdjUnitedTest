using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FdjAction.Common.Results;

namespace FdjUnited.Common.Results
{
    public class FdjUnitedActionResult<T> : IFdjUnitedActionResult
    {
        private HttpStatusCode _statusCode;

        public int Status
        {
            get => (int)_statusCode;
            set => _statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), value.ToString());
        }

        public virtual bool IsSuccess => Status >= (int)HttpStatusCode.OK && Status < (int)HttpStatusCode.Ambiguous;

        public T Data { get; set; }

        public IList<FdjUnitedActionError> Errors { get; set; }

        public FdjUnitedActionResult()
        {
        }

        public FdjUnitedActionResult(IFdjUnitedActionResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            Errors = result.Errors;
            Status = result.Status;
        }

        public FdjUnitedActionResult(HttpStatusCode statusCode, T data)
        {
            _statusCode = statusCode;
            Data = data;
        }

        public FdjUnitedActionResult(HttpStatusCode statusCode, FdjUnitedActionError error, T data)
         : this(statusCode, data)
        {
            Errors = new List<FdjUnitedActionError> { error };
        }

        public FdjUnitedActionResult(HttpStatusCode statusCode, IList<FdjUnitedActionError> errors, T data)
            : this(statusCode, data)
        {
            Errors = errors;
        }

        public FdjUnitedActionResult(int status, IList<FdjUnitedActionError> errors, T data)
        {
            Status = status;
            Errors = errors;
            Data = data;
        }

        public static implicit operator FdjUnitedActionResult<T>(FdjUnitedActionResult result)
        {
            return new FdjUnitedActionResult<T>(result);
        }
    }

    public class FdjUnitedActionResult : IFdjUnitedActionResult
    {
        private HttpStatusCode _statusCode;

        public int Status
        {
            get => (int)_statusCode;
            set => _statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), value.ToString());
        }

        public virtual bool IsSuccess => Status >= (int)HttpStatusCode.OK && Status < (int)HttpStatusCode.Ambiguous;

        public IList<FdjUnitedActionError> Errors { get; set; }

        public FdjUnitedActionResult()
        {
        }

        public FdjUnitedActionResult(IFdjUnitedActionResult result)
        {
            Errors = result.Errors;
            Status = result.Status;
        }

        public FdjUnitedActionResult(HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
        }

        public FdjUnitedActionResult(HttpStatusCode statusCode, FdjUnitedActionError error)
            : this(statusCode)
        {
            Errors = new List<FdjUnitedActionError> { error };
        }

        public FdjUnitedActionResult(HttpStatusCode statusCode, IEnumerable<FdjUnitedActionError> errors)
            : this(statusCode)
        {
            Errors = errors?.ToList();
        }

        public FdjUnitedActionResult(int status, IList<FdjUnitedActionError> errors)
        {
            Status = status;
            Errors = errors;
        }
    }
}
