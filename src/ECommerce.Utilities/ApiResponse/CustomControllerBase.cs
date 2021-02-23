using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Utilities.ApiResponse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Utilities.ApiResponse
{
    public abstract class CustomControllerBase : ControllerBase
    {
        private IResponseGenerator _responseGenerator;

        private IResponseGenerator ResponseGenerator
        {
            get
            {
                if (_responseGenerator != null) return _responseGenerator;
                var httpContext = HttpContext;
                IResponseGenerator resGenerator;
                if (httpContext == null)
                {
                    resGenerator = null;
                }
                else
                {
                    var requestServices = httpContext.RequestServices;
                    resGenerator = requestServices?.GetRequiredService<IResponseGenerator>();
                }

                _responseGenerator = resGenerator;

                return _responseGenerator;
            }
            set => _responseGenerator = value ?? throw new ArgumentNullException(nameof(value));
        }


        #region Validation ModelState

        [NonAction]
        public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            var errors = new List<ErrorMessage>();

            foreach (var error in ModelState)
            {
                if (error.Value.Errors.Count >= 1)
                {
                    errors.AddRange(error.Value.Errors.Select(er => new ErrorMessage
                    {
                        Property = error.Key,
                        Message = er.ErrorMessage
                    }));
                }
            }

            return BadRequest(new ErrorResponseTitleModel
            {
                Title = "One or more validation errors occurred.",
                Errors = errors
            });
        }

        #endregion
        

        #region 200-299

        [NonAction]
        public override OkObjectResult Ok(object value)
        {
            return base.Ok(value);
        }
        
        [NonAction]
        public override CreatedResult Created(string uri, object value)
        {
            return base.Created(uri, value);
        }
        [NonAction]
        public override CreatedResult Created(Uri uri, object value)
        {
            return base.Created(uri, value);
        }
        [NonAction]
        public override CreatedAtActionResult CreatedAtAction(string actionName, object value)
        {
            return base.CreatedAtAction(actionName, value);
        }
        [NonAction]
        public override CreatedAtActionResult CreatedAtAction(string actionName, object routeValues, object value)
        {
            return base.CreatedAtAction(actionName, routeValues, value);
        }
        [NonAction]
        public override CreatedAtActionResult CreatedAtAction(string actionName, string controllerName,
            object routeValues, object value)
        {
            return base.CreatedAtAction(actionName, controllerName, routeValues,
                value);
        }
        [NonAction]
        public override CreatedAtRouteResult CreatedAtRoute(string routeName, object value)
        {
            return base.CreatedAtRoute(routeName, value);
        }
        [NonAction]
        public override CreatedAtRouteResult CreatedAtRoute(object routeValues, object value)
        {
            return base.CreatedAtRoute(routeValues, value);
        }
        [NonAction]
        public override CreatedAtRouteResult CreatedAtRoute(string routeName, object routeValues, object value)
        {
            return base.CreatedAtRoute(routeName, routeValues, value);
        }
        [NonAction]
        public override AcceptedResult Accepted(object value)
        {
            return base.Accepted(value);
        }
        [NonAction]
        public override AcceptedResult Accepted(Uri uri, object value)
        {
            return base.Accepted(uri, value);
        }
        [NonAction]
        public override AcceptedAtActionResult AcceptedAtAction(string actionName, object value)
        {
            return base.AcceptedAtAction(actionName, value);
        }
        [NonAction]
        public override AcceptedAtActionResult AcceptedAtAction(string actionName, object routeValues, object value)
        {
            return base.AcceptedAtAction(actionName, routeValues, value);
        }
        [NonAction]
        public override AcceptedAtActionResult AcceptedAtAction(string actionName, string controllerName,
            object routeValues, object value)
        {
            return base.AcceptedAtAction(actionName, controllerName, routeValues,
                value);
        }
        [NonAction]
        public override AcceptedAtRouteResult AcceptedAtRoute(object routeValues, object value)
        {
            return base.AcceptedAtRoute(routeValues, value);
        }
        [NonAction]
        public override AcceptedAtRouteResult AcceptedAtRoute(string routeName, object routeValues, object value)
        {
            return base.AcceptedAtRoute(routeName, routeValues, value);
        }
        
        
        [NonAction]
        public override ObjectResult StatusCode(int statusCode, object value)
        {
            return base.StatusCode(statusCode, value);
        }

        #endregion

        #region Unauthorized

        [NonAction]
        public override UnauthorizedObjectResult Unauthorized(object value)
        {
            if (value == null)
            {
                value = new ErrorMessage();
            }

            var msg = (ErrorMessage) value;
            return base.Unauthorized(ResponseGenerator.GenerateErrorResponse(msg));
        }
        [NonAction]
        public UnauthorizedObjectResult Unauthorized(string message, string errorCode,string detail)
        {
            return base.Unauthorized(ResponseGenerator.GenerateErrorResponse(message, errorCode,detail));
        }
        [NonAction]
        public UnauthorizedObjectResult Unauthorized(List<ErrorMessage> messages)
        {
            return base.Unauthorized(ResponseGenerator.GenerateErrorResponse(messages));
        }

        #endregion

        #region NotFound

        [NonAction]

        public override NotFoundResult NotFound()
        {
            return base.NotFound();
        }
        
        [NonAction]

        public override NotFoundObjectResult NotFound(object value)
        {
            if (value == null)
            {
                value = new ErrorMessage();
            }

            var msg = (ErrorMessage) value;
            return base.NotFound(ResponseGenerator.GenerateErrorResponse(msg));
        }
        [NonAction]
        public NotFoundObjectResult NotFound(string message, string errorCode,string detail)
        {
            return base.NotFound(ResponseGenerator.GenerateErrorResponse(message, errorCode, detail));
        }
        [NonAction]
        public NotFoundObjectResult NotFound(List<ErrorMessage> messages)
        {
            return base.NotFound(ResponseGenerator.GenerateErrorResponse(messages));
        }

        #endregion

        #region BadRequest

        [NonAction]
        public override BadRequestObjectResult BadRequest(object value)
        {
            if (value == null)
            {
                value = new ErrorMessage();
            }

            var msg = (ErrorMessage) value;
            return base.BadRequest(ResponseGenerator.GenerateErrorResponse(msg));
        }
        [NonAction]
        public BadRequestObjectResult BadRequest(string message, string errorCode,string detail)
        {
            return base.BadRequest(ResponseGenerator.GenerateErrorResponse(message, errorCode, detail));
        }
        [NonAction]
        public BadRequestObjectResult BadRequest(List<ErrorMessage> messages)
        {
            return base.BadRequest(ResponseGenerator.GenerateErrorResponse( messages));
        }
        
        [NonAction]
        private BadRequestObjectResult BadRequest(ErrorResponseTitleModel errorModel)
        {
            return base.BadRequest(errorModel);
        }

        #endregion

        #region Conflict

        [NonAction]
        public override ConflictObjectResult Conflict(object value)
        {
            if (value == null)
            {
                value = new ErrorMessage();
            }

            var msg = (ErrorMessage) value;
            return base.Conflict(ResponseGenerator.GenerateErrorResponse(msg));
        }
        [NonAction]
        public ConflictObjectResult Conflict(string message, string errorCode,string detail)
        {
            return base.Conflict(ResponseGenerator.GenerateErrorResponse(message, errorCode, detail));
        }
        [NonAction]
        public ConflictObjectResult Conflict(List<ErrorMessage> messages)
        {
            return base.Conflict(ResponseGenerator.GenerateErrorResponse(messages));
        }

        #endregion

        
    }
}