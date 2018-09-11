using System;
using System.Configuration;

namespace TravelDeals.Framework.Helpers
{
    public static class Config
    {
        public static int GetRateLimitByAppId(string appId)
        {
            var rateLimit = ConfigurationManager.AppSettings[appId] == null ? ConfigurationManager.AppSettings["DefaultRateLimit"] : ConfigurationManager.AppSettings[appId];
            return Convert.ToInt32(rateLimit);
        }

        public static int DefaultExpiryTime
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultCacheExpiry"]);
            }
        }

        public static int PenaltyInMinutes
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["PenaltyInMinutes"]);
            }
        }
    }
}
