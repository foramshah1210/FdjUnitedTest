using MediatR;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FdjAction.Common.Results;
using FdjUnited.Common.Results;

namespace FdjUnited.Common.Handlers
{
     public abstract class BaseHandler
    {
        protected virtual FdjUnitedActionResult<T> FdjUnitedOk<T>(T data)
        {
            return new FdjUnitedActionResult<T>(HttpStatusCode.OK, data);
        }

        protected virtual FdjUnitedActionResult FdjUnitedOk()
        {
            return new FdjUnitedActionResult(HttpStatusCode.OK);
        }

        protected virtual FdjUnitedActionResult FdjUnitedNoContent()
        {
            return new FdjUnitedActionResult(HttpStatusCode.NoContent);
        }

        protected virtual FdjUnitedActionResult FdjUnitedUnauthorized()
        {
            return new FdjUnitedActionResult(HttpStatusCode.Unauthorized);
        }

        protected virtual FdjUnitedActionResult<T> FdjUnitedNotFoundObject<T>(T notFoundObject)
        {
            return new FdjUnitedActionResult<T>(HttpStatusCode.NotFound, notFoundObject);
        }

        protected virtual FdjUnitedActionResult FdjUnitedNotFound()
        {
            return FdjUnitedNotFound(new List<FdjUnitedActionError>());
        }

        protected virtual FdjUnitedActionResult FdjUnitedNotFound(IEnumerable<FdjUnitedActionError> errorMessages)
        {
            return new FdjUnitedActionResult(HttpStatusCode.NotFound, errorMessages);
        }

        protected virtual FdjUnitedActionResult FdjUnitedNotFound((string code, string error) message)
        {
            return FdjUnitedNotFound(new List<FdjUnitedActionError>
            {
                new FdjUnitedActionError(message.code, message.error),
            });
        }

        protected virtual FdjUnitedActionResult FdjUnitedNotFound(string code, string message)
        {
            return new FdjUnitedActionResult(HttpStatusCode.NotFound, new FdjUnitedActionError(code, message));
        }

        protected virtual FdjUnitedActionResult FdjUnitedCreated()
        {
            return new FdjUnitedActionResult(HttpStatusCode.Created);
        }

        protected virtual FdjUnitedActionResult<T> FdjUnitedCreated<T>(T value)
        {
            return new FdjUnitedActionResult<T>(HttpStatusCode.Created, value);
        }

        protected virtual FdjUnitedActionResult<T> FdjUnitedAccepted<T>(T value)
        {
            return new FdjUnitedActionResult<T>(HttpStatusCode.Accepted, value);
        }

        protected virtual FdjUnitedActionResult FdjUnitedAccepted()
        {
            return new FdjUnitedActionResult(HttpStatusCode.Accepted);
        }

        protected virtual FdjUnitedActionResult FdjUnitedConflict()
        {
            return new FdjUnitedActionResult(HttpStatusCode.Conflict);
        }

        protected virtual FdjUnitedActionResult FdjUnitedInternalServerError(string errorCode, string message = null)
        {
            var errors = new List<FdjUnitedActionError>();

            if (!string.IsNullOrWhiteSpace(errorCode + message))
            {
                errors.Add(new FdjUnitedActionError(errorCode, message));
            }

            return new FdjUnitedActionResult(HttpStatusCode.InternalServerError, errors);
        }

        protected virtual FdjUnitedActionResult<T> FdjUnitedInternalServerErrorObject<T>(T errorObject, string errorCode, string message = null)
        {
            var errors = new List<FdjUnitedActionError>();

            if (!string.IsNullOrWhiteSpace(errorCode + message))
            {
                errors.Add(new FdjUnitedActionError(errorCode, message));
            }

            return new FdjUnitedActionResult<T>(HttpStatusCode.InternalServerError, errors, errorObject);
        }

        protected virtual FdjUnitedActionResult FdjUnitedInternalServerError()
        {
            return FdjUnitedInternalServerError(null, null);
        }

        protected virtual FdjUnitedActionResult FdjUnitedBadRequest()
        {
            return FdjUnitedBadRequest(new List<FdjUnitedActionError>());
        }

        protected virtual FdjUnitedActionResult FdjUnitedBadRequest(string code, string message)
        {
            return FdjUnitedBadRequest(new List<FdjUnitedActionError>
            {
                new FdjUnitedActionError(code, message),
            });
        }

        protected virtual FdjUnitedActionResult FdjUnitedBadRequest((string code, string error) message)
        {
            return FdjUnitedBadRequest(new List<FdjUnitedActionError>
            {
                new FdjUnitedActionError(message.code, message.error),
            });
        }

        protected virtual FdjUnitedActionResult FdjUnitedBadRequest(string code)
        {
            return FdjUnitedBadRequest(new List<FdjUnitedActionError>
            {
                new FdjUnitedActionError(code),
            });
        }

        protected virtual FdjUnitedActionResult FdjUnitedBadRequest(FdjUnitedActionError errorMessage)
        {
            return FdjUnitedBadRequest(new List<FdjUnitedActionError>
            {
                errorMessage,
            });
        }

        protected virtual FdjUnitedActionResult FdjUnitedBadRequest(IEnumerable<FdjUnitedActionError> errorMessages)
        {
            return new FdjUnitedActionResult(HttpStatusCode.BadRequest, errorMessages);
        }

        protected virtual FdjUnitedActionResult<T> FdjUnitedBadRequestObject<T>(IList<FdjUnitedActionError> errorMessages, T errorObject)
        {
            return new FdjUnitedActionResult<T>(HttpStatusCode.BadRequest, errorMessages, errorObject);
        }

        protected virtual FdjUnitedActionResult FdjUnitedConflict(string code, string message)
        {
            return new FdjUnitedActionResult(HttpStatusCode.Conflict, new FdjUnitedActionError(code, message));
        }

        protected virtual FdjUnitedActionResult FdjUnitedConflict((string code, string error) message)
        {
            return new FdjUnitedActionResult(HttpStatusCode.Conflict, new FdjUnitedActionError(message.code, message.error));
        }
    }
     
    public abstract class BaseHandler<TRequest, TResponse> : BaseHandler, IRequestHandler<TRequest, FdjUnitedActionResult<TResponse>>
        where TRequest : IRequest<FdjUnitedActionResult<TResponse>>
    {
        public async Task<FdjUnitedActionResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var response = await HandleCommandAsync(request, cancellationToken);
            return response;
        }

        protected abstract Task<FdjUnitedActionResult<TResponse>> HandleCommandAsync(
            TRequest request,
            CancellationToken cancellationToken);
    }
}