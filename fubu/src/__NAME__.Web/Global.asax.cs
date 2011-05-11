using System;
using System.Web.Routing;
using __NAME__.Web.Configuration;
using __NAME__.Web.Configuration.Bootstrapping;
using FubuMVC.Core;
using FubuMVC.StructureMap;

namespace __NAME__.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            FubuApplication
                .For<__NAME__Registry>()
                .StructureMapObjectFactory(x => x.AddRegistry<__NAME__WebCoreRegistry>())
                .Bootstrap(RouteTable.Routes);
        }
    }
}