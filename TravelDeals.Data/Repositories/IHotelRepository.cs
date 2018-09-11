using System.Collections.Generic;
using System.Threading.Tasks;
using TravelDeals.Data.DbProvider;
using TravelDeals.Data.Extensions;

namespace TravelDeals.Data.Repositories
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> FetchAllHotelsAsync(DataSourceRequests request = null);
    }
}
