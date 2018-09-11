using System.Data.Entity;
using TravelDeals.Data.DbProvider;

namespace TravelDeals.Data.DbFactory
{
    public class DbFactory : IDbFactory
    {
        public hoteldbEntities1 dbContext;

        public DbContext DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    dbContext = new hoteldbEntities1();
                }

                return dbContext;
            }
        }
    }
}
