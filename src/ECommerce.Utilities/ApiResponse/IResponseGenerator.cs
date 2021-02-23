using System.Collections.Generic;
using ECommerce.Utilities.ApiResponse.Models;

namespace ECommerce.Utilities.ApiResponse
{
    public interface IResponseGenerator
    {
        ErrorResponseModel GenerateErrorResponse(string message, string errorCode, string detail, string property = null);
        ErrorResponseModel GenerateErrorResponse(ErrorMessage message);
        ErrorResponseModel GenerateErrorResponse(List<ErrorMessage> messages);
    }
}