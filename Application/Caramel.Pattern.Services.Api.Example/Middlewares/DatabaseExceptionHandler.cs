using Caramel.Pattern.Services.Domain.Entities.Models.Responses;
using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Extensions;
using Microsoft.AspNetCore.Diagnostics;
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

            var response = new ExceptionResponse(
                StatusProcess.Failure,
                $"{dbException} - {dbException.InnerException}"
            );

            httpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(response);

            return true;
        }
    }
}
