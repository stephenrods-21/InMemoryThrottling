using System;
using System.Runtime.Caching;
using TravelDeals.Framework.Helpers;
using TravelDeals.Framework.Models;
using TravelDeals.Framework.Utility;

namespace TravelDeals.Framework.Utils
{
    public class RequestThrottler
    {
        public bool IsThrottled(string key)
        {
            //Gets the rate limit by AppId from configuration file.
            int _rateLimit = Config.GetRateLimitByAppId(key);

            //Check to see if request for this appId is blocked for 5 minutes.
            ThrottleInfo throttleInfo = Caching.GetObjectFromCache<ThrottleInfo>($"{key}-BLOCKED");
            if (throttleInfo != null)
                return true;

            //Gets value from cache by appId, If does not exist then add in cache.
            throttleInfo = Caching.GetObjectFromCache<ThrottleInfo>(key);
            if (throttleInfo == null)
            {
                throttleInfo = new ThrottleInfo
                {
                    RequestCount = 1
                };

                Caching.AddInCache(key, throttleInfo);
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
                    Caching.AddInCache($"{key}-BLOCKED", new ThrottleInfo
                    { RequestCount = throttleInfo.RequestCount },
                    new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Config.PenaltyInMinutes) //penalty 5 minutes
                    });
                }
            }
            return throttleInfo.RequestCount > _rateLimit;
        }
    }
}
