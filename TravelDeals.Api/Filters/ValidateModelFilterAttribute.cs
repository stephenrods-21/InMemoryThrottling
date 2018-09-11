using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TravelDeals.Framework.Log;

namespace TravelDeals.Api.Filters
{
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var _logger = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(LoggerService)) as LoggerService;

            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
                _logger.LogInfo(actionContext.Response.ToString());
            }
            else if (actionContext.ActionArguments.Any(r => r.Value == null))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Request model cannot be null.");
                _logger.LogInfo(actionContext.Response.ToString());
            }
        }
    }
}