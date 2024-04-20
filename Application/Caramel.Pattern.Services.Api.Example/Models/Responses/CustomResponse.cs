using Caramel.Pattern.Services.Api.Example.Extensions;
using Caramel.Pattern.Services.Domain.Enums;

namespace Caramel.Pattern.Services.Api.Example.Models.Responses
{
    public class CustomResponse<T>
    {
        public CustomResponse(T data, StatusProcess status)
        {
            this.status = status;
            Description = status.GetDescription();
            Data = data;
        }

        public T Data { get; set; } 
        public StatusProcess status { get; set; }
        public string Description { get; set; }
    }
}
