using System;
using System.Runtime.Caching;
using TravelDeals.Framework.Helpers;

namespace TravelDeals.Framework.Utility
{
    public class Caching
    {
        private static ObjectCache cache = MemoryCache.Default;

        public static T GetObjectFromCache<T>(string cacheItemName)
        {
            return (T)cache[cacheItemName];
        }

        public static void AddInCache<T>(string cacheItemName, T cacheObject, CacheItemPolicy cachePolicy = null)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(Config.DefaultExpiryTime)
            };

            cache.Set(cacheItemName, cacheObject, cachePolicy != null ? cachePolicy : cacheItemPolicy);
        }
    }
}
