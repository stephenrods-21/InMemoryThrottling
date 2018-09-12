using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TravelDeals.Data.DbProvider;
using TravelDeals.Data.Extensions;

namespace TravelDeals.Data.Repositories
{
    public class HotelRepository : IHotelRepository
    {

        public Task<List<Hotel>> FetchAllHotelsAsync(DataSourceRequests request = null)
        {
            IEnumerable<Hotel> result = this.GetHotels();
            if (request.Search != string.Empty)
            {
                result = result.Where(r => r.CITY.StartsWith(request.Search)).ToList();
            }

            if (request.Sort.ToUpper() == "DESC")
            {
                result = result.OrderByDescending(r => r.PRICE).ToList();
            }
            else
            {
                result = result.OrderBy(r => r.PRICE).ToList();
            }

            return Task.FromResult(result.ToList());
        }

        private List<Hotel> GetHotels()
        {
            List<Hotel> hotels = new List<Hotel>();

            using (TextReader reader = File.OpenText(@"C:\hoteldb.csv"))
            {
                CsvReader csv = new CsvReader(reader);
                csv.Configuration.Delimiter = ",";
                csv.Configuration.MissingFieldFound = null;
                while (csv.Read())
                {
                    Hotel Record = csv.GetRecord<Hotel>();
                    hotels.Add(Record);
                }
            }
            return hotels;
        }
    }
}
