using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelDeals.Framework.Log
{
    public class LoggerService
    {
        private readonly Logger logger = LogManager.GetLogger("databaseLogger");
        public void LogError(Exception ex)
        {
            logger.Error(ex);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }
    }
}
