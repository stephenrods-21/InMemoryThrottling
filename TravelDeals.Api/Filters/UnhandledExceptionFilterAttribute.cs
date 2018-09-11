using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using TravelDeals.Framework.Log;

namespace TravelDeals.Api.Filters
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var _logger = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(LoggerService)) as LoggerService;
            _logger.LogError(context.Exception);
            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}