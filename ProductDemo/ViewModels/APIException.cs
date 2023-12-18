using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net;

namespace ProductDemo.ViewModels
{
    public class APIException: Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string AdditionalErrorMessage { get; set; }
        public override string Message { get; }
        public APIException(HttpStatusCode statusCode, string? additionalErrorMessage = null, string? message = null):base(message)
        {
            StatusCode = statusCode;
            AdditionalErrorMessage = additionalErrorMessage;
            Message = message;
        }
        public APIException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
