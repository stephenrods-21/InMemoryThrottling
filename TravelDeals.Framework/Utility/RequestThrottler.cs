using System;
using System.Collections.Concurrent;
using TravelDeals.Framework.Helpers;
using TravelDeals.Framework.Models;

namespace TravelDeals.Framework.Utils
{
    public static class RequestThrottler
    {
        public static ConcurrentDictionary<string, ThrottleInfo> _cache = new ConcurrentDictionary<string, ThrottleInfo>();

        public static bool IsThrottled(string key)
        {
            //Gets the rate limit by AppId from configuration file.
            int _rateLimit = Config.GetRateLimitByAppId(key);

            //Check to see if request for this appId is blocked for 5 minutes.
            _cache.TryGetValue($"{key}-BLOCKED", out ThrottleInfo throttled);
            if (throttled != null && throttled.Expiry > DateTime.Now)
                return true;
            else
                _cache.TryRemove($"{key}-BLOCKED", out ThrottleInfo _);

            //Gets value from cache by appId, If does not exist then add in cache.
            _cache.TryGetValue(key, out ThrottleInfo throttleInfo);


            if (throttleInfo != null && throttleInfo.Expiry <= DateTime.Now)
            {
                throttleInfo = new ThrottleInfo();
                throttleInfo.Expiry = DateTime.Now.AddSeconds(Config.DefaultExpiryTime);

                _cache.AddOrUpdate(key, throttleInfo, (oldVal, newVal) => throttleInfo);
            }

            if (throttleInfo == null)
            {
                throttleInfo = new ThrottleInfo
                {
                    RequestCount = 1,
                    Expiry = DateTime.Now.AddSeconds(Config.DefaultExpiryTime)
                };

                _cache.AddOrUpdate(key, throttleInfo, (oldVal, newVal) => throttleInfo);
            }
            else
            {
                //updates the request count by 1 in the cache per request.
                throttleInfo.RequestCount++;

                //Check if the appId has exceeded the number of requests.
                //If yes then add a new key in the cache with suffix "{appId}-BLOCKED" and set expiry to 5 minutes
                //This means all request for "{appId}" will be temporarily blocked for 5 minutes.
                if (throttleInfo.RequestCount > _rateLimit)
                {
                    throttleInfo.Expiry = DateTime.Now.AddMinutes(Config.PenaltyInMinutes); //Penalty 5 minutes.
                    _cache.TryAdd($"{key}-BLOCKED", throttleInfo);
                }
            }
            return throttleInfo.RequestCount > _rateLimit;
        }
    }
}
