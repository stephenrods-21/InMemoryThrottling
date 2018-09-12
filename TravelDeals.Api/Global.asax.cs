using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TravelDeals.Data.DbFactory;
using TravelDeals.Data.Repositories;
using TravelDeals.Framework.Models;
using TravelDeals.Framework.Utils;
using TravelDeals.Service.Business;

namespace TravelDeals.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Timer _timmer = new Timer();
            _timmer.Elapsed += new ElapsedEventHandler(OnTimmedEvent);
            _timmer.Interval = 100000;
            _timmer.Enabled = true;

            this.IntializeSimpleInjector();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void OnTimmedEvent(object source, ElapsedEventArgs e)
        {
            var expiredKeys = (from c in RequestThrottler._cache
                               where c.Value.Expiry < DateTime.Now
                               select c.Key).ToList();

            foreach (var key in expiredKeys)
            {
                ThrottleInfo _;
                RequestThrottler._cache.TryRemove(key, out _);
            }
        }

        private void IntializeSimpleInjector()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<IHotelService, HotelService>();
            container.Register<IHotelRepository, HotelRepository>();
            container.Register<IDbFactory, DbFactory>();

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
