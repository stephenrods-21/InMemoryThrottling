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
            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}