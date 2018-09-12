using System.Collections.Generic;
using System.Data.Entity;
using TravelDeals.Data.DbProvider;

namespace TravelDeals.Data.DbFactory
{
    public interface IDbFactory
    {
        List<Hotel> DbContext { get; }
    }
}
