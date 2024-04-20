using Caramel.Pattern.Services.Api.Example.Extensions;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace Caramel.Pattern.Services.Api.Example.Middlewares
{
    public class DatabaseExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BusinessExceptionHandler> _logger;

        public DatabaseExceptionHandler(ILogger<BusinessExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not DbException dbException)
                return false;

            _logger.LogWarning(exception, StatusProcess.DbFailure.GetDescription(), exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = (int)StatusProcess.DbFailure,
                Extensions = new Dictionary<string, object?>()
                {
                    { "Description",  StatusProcess.DbFailure.GetDescription() },
                    { "ErrorDetails",  dbException.Message }
                }                
            };

            httpContext.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);

            return true;
        }
    }
}
