using Caramel.Pattern.Services.Domain.Enums;
using Caramel.Pattern.Services.Domain.Extensions;

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

        public T Data { get; set; }
        public StatusProcess Status { get; set; }
        public string Description { get; set; }
    }
}
