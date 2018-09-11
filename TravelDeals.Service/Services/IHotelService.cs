using System.Threading.Tasks;
using TravelDeals.Data.Extensions;
using TravelDeals.Framework.ViewModels;

namespace TravelDeals.Service.Business
{
    public interface IHotelService
    {
        Task<HotelListVM> FetchAllHotels(DataSourceRequests req);
    }
}
