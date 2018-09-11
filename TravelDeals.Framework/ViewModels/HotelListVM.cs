using System.Collections.Generic;
using TravelDeals.Data.DbProvider;

namespace TravelDeals.Framework.ViewModels
{
    public class HotelListVM
    {
        public List<Hotel> Hotels { get; set; }

        public int TotalRecords { get; set; }
    }
}
