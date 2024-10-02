using Application.Common.Exceptions;
using Application.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;

namespace SaveLogAPI.Infrastructure
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;
        public CustomExceptionHandler()
        {
            _exceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
        };
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionType = exception.GetType();
            if (_exceptionHandlers.ContainsKey(exceptionType))
            {
                await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
                return true;
            }
            else
                await HandleUnknowException(httpContext, exception);

            return false;
        }
        private async Task HandleValidationException(HttpContext context, Exception ex)
        {
            var exception = (ValidationException)ex;

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(Response<string>.FailedResult(exception.ErrorMessage));
        }
        private async Task HandleUnknowException(HttpContext httpContext, Exception exception)
        {
            string msgTxt;
            if (exception.InnerException != null && !String.IsNullOrEmpty(exception.InnerException.Message))
                msgTxt = exception.InnerException.Message;
            else
                msgTxt = exception.Message;

            var st = new StackTrace(exception, true);
            var frame = st.GetFrame(0);
            var file = frame.GetFileName();
            var line = frame.GetFileLineNumber();

            msgTxt = $"{exception.GetType().Name}: {msgTxt})";

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(Response<string>.FailedResult(msgTxt));
        }
        private async Task HandleUnauthorizedAccessException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await context.Response.WriteAsJsonAsync(Response<string>.FailedResult(ex.Message));
        }
    }
}
