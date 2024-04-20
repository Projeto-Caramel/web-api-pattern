using Caramel.Pattern.Services.Api.Example.Extensions;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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

            var problemDetails = new ProblemDetails
            {
                Status = (int)businessException.Status,
                Extensions = new Dictionary<string, object?>()
                {
                    { "Description",  businessException.Status.GetDescription() },
                    { "ErrorDetails",  businessException.ErrorDetails }
                }                
            };

            httpContext.Response.StatusCode = (int)businessException.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);

            return true;
        }
    }
}
