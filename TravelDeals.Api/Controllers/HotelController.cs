using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TravelDeals.Api.Filters;
using TravelDeals.Data.Extensions;
using TravelDeals.Framework.Utils;
using TravelDeals.Service.Business;

namespace TravelDeals.Api.Controllers
{
    [RoutePrefix("api/hotel")]
    [UnhandledExceptionFilter]
    public class HotelController : ApiController
    {
        private readonly IHotelService _hotelService;
        private readonly RequestThrottler _requestThrottler;

        public HotelController(IHotelService hotelService,
            RequestThrottler requestThrottler)
        {
            _hotelService = hotelService;
            _requestThrottler = requestThrottler;
        }

        [ValidateModelFilter]
        [HttpPost, Route("list")]
        public async Task<HttpResponseMessage> FetchAllHotelAsync(DataSourceRequests request)
        {
            if (_requestThrottler.IsThrottled(request.ApiKey))
            {
                return Request.CreateResponse((HttpStatusCode)429, "Too many requests.");
            }

            var result = await _hotelService.FetchAllHotels(request);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
