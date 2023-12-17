using System.Text.Json;

namespace ProductDemo.ViewModels
{
    public class ErrorResponse
    {
        public int ResponseCode { get; set; }
        public string? ResponseMessage { get; set;}
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
