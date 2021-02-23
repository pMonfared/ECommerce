using System.Collections.Generic;
using Newtonsoft.Json;

namespace ECommerce.Utilities.ApiResponse.Models
{
    //https://tools.ietf.org/html/rfc7807
    public class ErrorResponseTitleModel : ErrorResponseModel
    {
        [JsonProperty("title")] public string Title { get; set; }
    }
    
    public class ErrorResponseModel
    {
        [JsonProperty("errors")] public List<ErrorMessage> Errors { get; set; }
    }
    
    public class ErrorMessage
    {
        [JsonProperty("code")] public string Code { get; set; } = "NoCode";
        [JsonProperty("property")]  public string Property { get; set; }
        [JsonProperty("message")]  public string Message { get; set; }
        [JsonProperty("detail")] public string Detail { get; set; } = "NoDetail";
    }

}