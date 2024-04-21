using Caramel.Pattern.Services.Domain.Entities.Models.Responses;
using Caramel.Pattern.Services.Domain.Exceptions;
using Caramel.Pattern.Services.Domain.Extensions;
using Microsoft.AspNetCore.Diagnostics;

namespace Caramel.Pattern.Services.Api.Example.Middlewares
{
    public class BusinessExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BusinessExceptionHandler> _logger;

        public BusinessExceptionHandler(ILogger<BusinessExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not BusinessException businessException)
                return false;

            _logger.LogWarning(exception, businessException.Status.GetDescription(), exception.Message);

            var response = new ExceptionResponse(
                businessException.Status,
                businessException.ErrorDetails
            );

            httpContext.Response.StatusCode = (int)businessException.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response);

            return true;
        }
    }
}
