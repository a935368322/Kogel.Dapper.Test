using Kogel.Dapper.Extension.Core.SetQ;
using Kogel.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Kogel.Dapper.Extension;

namespace Kogel.Framework.Test
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //预加载实体类
            EntityCache.Register((new Type[] { typeof(users), typeof(project_Role) }));
            
        }
    }
}
