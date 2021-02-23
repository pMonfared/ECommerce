using System.Collections.Generic;
using ECommerce.Utilities.ApiResponse.Models;

namespace ECommerce.Utilities.ApiResponse
{
    public class ResponseGenerator : IResponseGenerator
    {
        public ErrorResponseModel GenerateErrorResponse(string message, string errorCode, string detail, string property = null)
        {
            return new ErrorResponseModel
            {
                Errors = new List<ErrorMessage>
                {
                    new ErrorMessage
                    {
                        Code = errorCode,
                        Message = message,
                        Property = property,
                        Detail = detail
                    }
                }
            };
        }

        public ErrorResponseModel GenerateErrorResponse(ErrorMessage message)
        {
            return new ErrorResponseModel
            {
                Errors = new List<ErrorMessage>
                {
                    message
                }
            };
        }

        public ErrorResponseModel GenerateErrorResponse(List<ErrorMessage> messages)
        {
            if (messages == null)
            {
                messages = new List<ErrorMessage>();
            }

            return new ErrorResponseModel
            {
                Errors = messages
            };
        }


    }
}