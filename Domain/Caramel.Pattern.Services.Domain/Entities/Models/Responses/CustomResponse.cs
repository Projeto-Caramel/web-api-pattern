using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Extensions;
using System.Text.Json.Serialization;

namespace Caramel.Pattern.Services.Domain.Entities.Models.Responses
{
    public class CustomResponse<T>
    {
        public CustomResponse(T data, StatusProcess status)
        {
            this.Status = status;
            Description = status.GetDescription();
            Data = data;
        }

        public CustomResponse() { }

        [JsonPropertyName("data")]
        public T Data { get; set; }
        [JsonPropertyName("status")]
        public StatusProcess Status { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
