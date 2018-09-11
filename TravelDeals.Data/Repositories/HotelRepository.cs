using LinqKit;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TravelDeals.Data.DbFactory;
using TravelDeals.Data.DbProvider;
using TravelDeals.Data.Extensions;

namespace TravelDeals.Data.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        protected DbContext _dbContext;
        protected DbSet<Hotel> _dbSet;

        public HotelRepository(IDbFactory dbFactory)
        {
            _dbContext = dbFactory.DbContext;
            _dbSet = _dbContext.Set<Hotel>();
        }

        /// <summary>
        /// Returns list of hotels with filtering & sorting
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<Hotel>> FetchAllHotelsAsync(DataSourceRequests request = null)
        {
            var predicate = this.BuildPredicate(request);
            IQueryable<Hotel> query;

            if (predicate == null)
            {
                query = _dbSet.AsExpandable();
            }
            else
            {
                query = _dbSet.AsExpandable().Where(predicate);
            }

            var result = this.AddSorting(query, request.Sort);
            return await result.ToListAsync();
        }

        //Query Builder to apply filter dynamically
        private ExpressionStarter<Hotel> BuildPredicate(DataSourceRequests req)
        {
            ExpressionStarter<Hotel> predicate = null;
            if (req != null && req.Search != string.Empty)
            {
                predicate = PredicateBuilder.New<Hotel>();

                if (req.Search != string.Empty)
                    predicate = predicate.And(x => x.CITY.StartsWith(req.Search));
            }
            return predicate;
        }

        //Applies sorting on price field to the query.
        private IQueryable<Hotel> AddSorting(IQueryable<Hotel> query, string sort)
        {
            switch (sort.ToUpper())
            {
                case "DESC":
                    query = query.OrderByDescending(r => r.PRICE);
                    break;

                case "ASC":
                    query = query.OrderBy(r => r.PRICE);
                    break;

                default:
                    query = query.OrderBy(r => r.PRICE);
                    break;
            }
            return query;
        }
    }
}
