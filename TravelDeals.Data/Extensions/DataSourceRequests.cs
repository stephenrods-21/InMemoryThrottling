using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelDeals.Data.Extensions
{
    public class DataSourceRequests
    {
        public DataSourceRequests()
        {
            Search = string.Empty;
            Sort = string.Empty;
        }

        [Required]
        public string ApiKey { get; set; }

        public string Search { get; set; }

        public string Sort { get; set; }
    }
}
