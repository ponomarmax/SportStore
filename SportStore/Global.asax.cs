using Ninject;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IKernel kernel = new StandardKernel();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new Infrastructure.NinjectDependencyResolver(kernel));
        }
    }
}
