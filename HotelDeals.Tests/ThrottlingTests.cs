using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelDeals.Data.Extensions;
using TravelDeals.Data.Repositories;
using TravelDeals.Framework.Helpers;
using TravelDeals.Framework.Utils;
using TravelDeals.Service.Business;

namespace HotelDeals.Tests
{
    [TestClass]
    public class ThrottlingTests
    {
        private string apiKey = string.Empty;

        [TestMethod]
        public void WhenNumberOfRequestIsEqualToRateLimit()
        {
            //key TEST100 has rate Limit as 15
            apiKey = "TEST102";

            int rateLimit = Config.GetRateLimitByAppId(apiKey);
            int success = 0;
            for (int i = 1; i <= rateLimit; i++)
            {
                if (!RequestThrottler.IsThrottled(apiKey))
                {
                    success++;
                }
            }

            Assert.IsTrue(rateLimit == success);
        }

        [TestMethod]
        public void WhenNumberOfRequestIsGreaterThanRateLimit()
        {
            //key TEST101 has rate Limit as 20
            apiKey = "TEST100";
            int success = 0, failed = 0;

            int rateLimit = Config.GetRateLimitByAppId(apiKey);

            for (int i = 1; i <= rateLimit; i++)
            {
                if (!RequestThrottler.IsThrottled(apiKey))
                {
                    success++;
                }
                else
                {
                    failed++;
                }
            }

            Assert.IsTrue(rateLimit - success == failed);
        }

        [TestMethod]
        public void WhenAppIdNotExistsInConfigAssignDefaultRate()
        {
            //Default rate limit is 10 in config.
            apiKey = "XXZZZZZ";
            int success = 0;

            int rateLimit = Config.GetRateLimitByAppId(apiKey);

            for (int i = 1; i <= rateLimit; i++)
            {
                if (!RequestThrottler.IsThrottled(apiKey))
                {
                    success++;
                }
            }

            Assert.IsTrue(success == rateLimit);
        }

        [TestMethod]
        public void MultipleRequestWithSameApiKey()
        {
            //key TEST100 has rate Limit as 15
            apiKey = "TEST0000";

            int rateLimit = Config.GetRateLimitByAppId(apiKey);
            int successA = 0, successB = 0, successC = 0;
            for (int i = 1; i <= rateLimit; i++)
            {
                if (!RequestThrottler.IsThrottled(apiKey))
                {
                    successA++;
                }

                if (!RequestThrottler.IsThrottled(apiKey))
                {
                    successB++;
                }

                if (!RequestThrottler.IsThrottled(apiKey))
                {
                    successC++;
                }
            }

            Assert.IsTrue((successA + successB + successC) == rateLimit);
        }

        [TestMethod]
        public async Task TryGetHotels()
        {
            HotelService _hotelService = new HotelService(new HotelRepository());
            DataSourceRequests req = new DataSourceRequests
            {
                ApiKey = "TEST200",
                Sort = "DESC"
            };

            var result = await _hotelService.FetchAllHotels(req);

            Assert.IsTrue(result.TotalRecords > 0);
        }

        [TestMethod]
        public async Task SortByPriceHighToLow()
        {
            bool sorted = false;
            HotelService _hotelService = new HotelService(new HotelRepository());
            DataSourceRequests req = new DataSourceRequests
            {
                ApiKey = "TESTDESC",
                Sort = "DESC"
            };

            var result = await _hotelService.FetchAllHotels(req);

            for (int i = 0; i < result.Hotels.Count - 1; i++)
            {
                if (result.Hotels[i].PRICE >= result.Hotels[i + 1].PRICE)
                    sorted = true;
            }

            Assert.IsTrue(sorted);
        }

        [TestMethod]
        public async Task SortByPriceLowToHigh()
        {
            bool sorted = false;
            HotelService _hotelService = new HotelService(new HotelRepository());
            DataSourceRequests req = new DataSourceRequests
            {
                ApiKey = "TESTASC",
                Sort = "ASC"
            };

            var result = await _hotelService.FetchAllHotels(req);

            for (int i = 0; i < result.Hotels.Count - 1; i++)
            {
                if (result.Hotels[i].PRICE <= result.Hotels[i + 1].PRICE)
                    sorted = true;
            }

            Assert.IsTrue(sorted);
        }

        [TestMethod]
        public void WhenRateLimitExceedsCheckIfKeyIsBlocked()
        {
            //key HD700 has default rate Limit 10
            apiKey = "HD700";
            bool isBlocked = false;
            for (int i = 1; i <= 11; i++)
            {
                if (RequestThrottler.IsThrottled(apiKey))
                {
                    if (RequestThrottler._cache.ContainsKey($"{apiKey}-BLOCKED"))
                        isBlocked = true;
                }
            }
            Assert.IsTrue(isBlocked);
        }

        [TestMethod]
        public async Task TryGetAllHotelsInBangkokOnly()
        {
            int hotelInBangkokCount = 0;
            HotelService _hotelService = new HotelService(new HotelRepository());
            DataSourceRequests req = new DataSourceRequests
            {
                ApiKey = "TESTASC",
                Search = "Bangkok",
                Sort = "ASC"
            };

            var result = await _hotelService.FetchAllHotels(req);

            for (int i = 0; i < result.Hotels.Count; i++)
            {
                if (result.Hotels[i].CITY.Equals("Bangkok"))
                    hotelInBangkokCount++;
            }

            Assert.IsTrue(hotelInBangkokCount == result.TotalRecords);
        }
    }
}
