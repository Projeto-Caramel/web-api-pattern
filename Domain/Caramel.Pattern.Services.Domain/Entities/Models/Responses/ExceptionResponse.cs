using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Extensions;

namespace Caramel.Pattern.Services.Domain.Entities.Models.Responses
{
    public class ExceptionResponse
    {
        public StatusProcess Status { get; set; }
        public string Description { get; set; }
        public object ErrorDetails { get; set; }

        public ExceptionResponse(StatusProcess status, object details)
        {
            Status = status;
            Description = status.GetDescription();
            ErrorDetails = details;
        }
    }
}
