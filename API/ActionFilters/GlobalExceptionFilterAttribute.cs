using LMS.Common.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using Parachute.SaaS.Common.Helper;
using Parachute.SaaS.Common.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;

namespace Parachute.SaaS.API.ActionFilters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Error(context.Request, "Controller : " + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception);

            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ValidationException))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(context.Exception.Message), ReasonPhrase = "ValidationException", };
                throw new HttpResponseException(resp);

            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            else if (exceptionType == typeof(APIException))
            {
                var webapiException = context.Exception as APIException;
                ApiResponse response = new ApiResponse();
                response.ErrorMessages = new System.Collections.Generic.List<string>();
                response.ErrorMessages.Add(webapiException.Message);
                response.ErrorMessages.Add(webapiException?.InnerException?.Message);
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;                
                context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, response);
                //throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }
    }
}