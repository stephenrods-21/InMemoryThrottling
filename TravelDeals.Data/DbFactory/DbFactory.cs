using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using TravelDeals.Data.DbProvider;

namespace TravelDeals.Data.DbFactory
{
    public class DbFactory : IDbFactory
    {
        public List<Hotel> dbContext;

        public List<Hotel> DbContext
        {
            get
            {
                return this.GetHotels();
            }
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


            //using (var rd = new StreamReader(@"C:\hoteldb.csv"))
            //{
            //    int i = 0;
            //    while (!rd.EndOfStream)
            //    {
            //        if (i > 2)
            //        {
            //            var splits = rd.ReadLine().Split(',');
            //            hotels.Add(new Hotel { CITY = splits[0], HOTELID = Convert.ToInt32(splits[1]), PRICE = Convert.ToInt32(splits[2]), ROOM = splits[3] });
            //        }
            //        i++;
            //    }
            //}
            return hotels;

            //List<Hotel> _hotels = new List<Hotel>();
            //_hotels.Add(new Hotel { CITY = "Bangkok", HOTELID = 1, PRICE = 100, ROOM = "Deluxe" });
            //_hotels.Add(new Hotel { CITY = "Amsterdam", HOTELID = 2, PRICE = 0, ROOM = "Superior" });
            //_hotels.Add(new Hotel { CITY = "Ashburn", HOTELID = 3, PRICE = 0, ROOM = "Sweet Suite" });
            //_hotels.Add(new Hotel { CITY = "Amsterdam", HOTELID = 4, PRICE = 0, ROOM = "Superior" });
            //_hotels.Add(new Hotel { CITY = "Ashburn", HOTELID = 5, PRICE = 0, ROOM = "Deluxe" });
            //_hotels.Add(new Hotel { CITY = "Bangkok", HOTELID = 6, PRICE = 200, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "Ashburn", HOTELID = 7, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "Bangkok", HOTELID = 8, PRICE = 300, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "Amsterdam", HOTELID = 9, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "Ashburn", HOTELID = 10, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "Bangkok", HOTELID = 11, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "Ashburn", HOTELID = 12, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 13, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 14, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 15, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 16, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 17, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 18, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 19, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 20, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 21, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 22, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 23, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 24, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 25, PRICE = 0, ROOM = "" });
            //_hotels.Add(new Hotel { CITY = "", HOTELID = 26, PRICE = 0, ROOM = "" });

            //return _hotels;
        }
    }

}
