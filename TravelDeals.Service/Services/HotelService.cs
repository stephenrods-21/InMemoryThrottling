using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelDeals.Data.DbProvider;
using TravelDeals.Data.Extensions;
using TravelDeals.Data.Repositories;
using TravelDeals.Framework.Log;
using TravelDeals.Framework.ViewModels;

namespace TravelDeals.Service.Business
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<HotelListVM> FetchAllHotels(DataSourceRequests req)
        {
            var response = new HotelListVM();
            response.Hotels = await _hotelRepository.FetchAllHotelsAsync(req);
            response.TotalRecords = response.Hotels.Count();
            return response;
        }
    }
}