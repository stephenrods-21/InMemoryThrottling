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

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [ValidateModelFilter]
        [HttpPost, Route("list")]
        public async Task<HttpResponseMessage> FetchAllHotelAsync(DataSourceRequests request)
        {
            if (RequestThrottler.IsThrottled(request.ApiKey))
            {
                return Request.CreateResponse((HttpStatusCode)429, "Too many requests.");
            }

            var result = await _hotelService.FetchAllHotels(request);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
