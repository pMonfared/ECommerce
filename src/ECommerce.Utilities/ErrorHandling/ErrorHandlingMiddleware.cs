using System;
using System.Net;
using System.Threading.Tasks;
using ECommerce.Utilities.ApiResponse;
using ECommerce.Utilities.ApiResponse.Models;
using ECommerce.Utilities.Consts;
using ECommerce.Utilities.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ECommerce.Utilities.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;


        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        private async Task GenerateResponse(string message, HttpContext context, string errorCode, int stsCode = 0, string detail = null, string property = null, bool isError = false)
        {
            var requestServices = context.RequestServices;
            var responseGenerator = requestServices.GetRequiredService<IResponseGenerator>();

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(responseGenerator.GenerateErrorResponse(message, errorCode, detail, property));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = stsCode == 0 ? (int) HttpStatusCode.BadRequest : stsCode;

            await context.Response.WriteAsync(result);
        }


        public async Task Invoke(HttpContext context, 
            ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                logger.LogError(
                    $"Exception : {ex.Id} type of {ex.GetType()} withCode {ex.ExceptionDetails.ErrorCode} Occur in : {ex.ExceptionDetails.OccurAddress}\n" +
                    $"{ex.Message}\n" +
                    $"{ex.InnerException?.Message}\n" +
                    $"Trace :\n" +
                    $"{ex.StackTrace}\n" +
                    $"Inner Trace :\n" +
                    $"{ex.InnerException?.StackTrace}\n" +
                    $"End Exception {ex.Id}");

                if (ex.ExceptionDetails.IsHighPriority)
                {
                    logger.LogError(ex.StackTrace);
                }

                string msgText;
                string errorCode;
                var statusCode = 0;
                string detailText;
                var property = ex?.ExceptionDetails?.Property;

                switch (ex)
                {
                    case CustomNotFoundException _:
                        statusCode = StatusCodes.Status404NotFound;
                        detailText = ex?.ExceptionDetails?.Detail ?? Errors.General.NotFound.Detail;
                        msgText = ex?.ExceptionDetails?.Message ?? Errors.General.NotFound.Message;
                        errorCode = ex?.ExceptionDetails?.ErrorCode ?? Errors.General.NotFound.ErrorCode;
                        break;
                    case CustomForbiddenException _:
                        statusCode = StatusCodes.Status403Forbidden;
                        detailText = ex?.ExceptionDetails?.Detail ?? Errors.General.ForbiddenAccessDenied.Detail;
                        msgText = ex?.ExceptionDetails?.Message ?? Errors.General.ForbiddenAccessDenied.Message;
                        errorCode = ex?.ExceptionDetails?.ErrorCode ??Errors.General.ForbiddenAccessDenied.ErrorCode;
                        break;
                    case CustomArgumentOutOfRangeException _:
                        statusCode = StatusCodes.Status400BadRequest;
                        detailText = ex?.ExceptionDetails?.Detail ?? Errors.General.ArgumentOutOfRange.Detail;
                        msgText = ex?.ExceptionDetails?.Message ?? Errors.General.ArgumentOutOfRange.Message;
                        errorCode = ex?.ExceptionDetails?.ErrorCode ??Errors.General.ArgumentOutOfRange.ErrorCode;
                        break;
                    default:
                        msgText = ex?.ExceptionDetails?.Message ?? Errors.General.InternalServer.Message;
                        errorCode = ex?.ExceptionDetails?.ErrorCode ?? Errors.General.InternalServer.ErrorCode;
                        detailText = ex?.ExceptionDetails?.Detail ?? Errors.General.InternalServer.Detail;
                        break;
                }
                
                await GenerateResponse(msgText, context, errorCode, statusCode, detailText, property);
            }
            catch (Exception ex)
            {

                logger.LogError(
                    $"Unmoral Exception : [NO_ID] type of {ex.GetType()} withCode [NO_CODE] Occur in : {ex.Source}\n" +
                    $"{ex.Message}\n" +
                    $"{ex.InnerException?.Message}\n" +
                    $"Trace :\n" +
                    $"{ex.StackTrace}\n" +
                    $"End Exception [NO_ID]");

                switch (ex)
                {
                    case NullReferenceException nullReferenceException:
                        break;
                    case OutOfMemoryException outOfMemoryException:
                        break;
                    case OverflowException overflowException:
                        break;
                }

                await GenerateResponse(
                    Errors.General.InternalServer.Message, 
                    context, 
                    Errors.General.InternalServer.ErrorCode,
                    StatusCodes.Status500InternalServerError,
                    Errors.General.InternalServer.Detail);
            }
        }
    }
}