using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelDeals.Framework.Models
{
    public class ThrottleInfo
    {
        public int RequestCount { get; set; }

        public DateTime Expiry { get; set; }
    }
}
