using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Extensions;
using System.Text.Json.Serialization;

namespace Caramel.Pattern.Services.Domain.Entities.Models.Responses
{
    public class ExceptionResponse
    {
        [JsonPropertyName("status")]
        public StatusProcess Status { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("errorDetails")]
        public object ErrorDetails { get; set; }

        public ExceptionResponse(StatusProcess status, object details)
        {
            Status = status;
            Description = status.GetDescription();
            ErrorDetails = details;
        }
    }
}
