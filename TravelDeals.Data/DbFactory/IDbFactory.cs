using System.Data.Entity;

namespace TravelDeals.Data.DbFactory
{
    public interface IDbFactory
    {
        DbContext DbContext { get; }
    }
}
