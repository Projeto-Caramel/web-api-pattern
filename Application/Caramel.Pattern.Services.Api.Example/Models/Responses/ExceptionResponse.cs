using Caramel.Pattern.Services.Api.Example.Extensions;
using Caramel.Pattern.Services.Domain.Enums;

namespace Caramel.Pattern.Services.Api.Example.Models.Responses
{
    public class ExceptionResponse<T>
    {
        public StatusProcess Status { get; set; }
        public string Description { get; set; }
        public T ErrorDetails { get; set; }

        public ExceptionResponse(string message, StatusProcess status, T? details = default)
        {
            Status = status;
            Description = status.GetDescription();
            ErrorDetails = details;
        }
    }
}
