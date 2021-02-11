using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using poc_hibernate.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace poc_hibernate.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var connectionString = "Data Source=.;Initial Catalog=NhibernateDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var config = new Configuration();

            config.DataBaseIntegration(x =>
            {
                x.ConnectionString = connectionString;
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
            });

            config.AddAssembly(typeof(DataBaseProvider).Assembly.GetName().Name);

            var sessionFactory = config.BuildSessionFactory();

            DataBaseProvider.SetSessionFactory(sessionFactory);
        }
    }
}
