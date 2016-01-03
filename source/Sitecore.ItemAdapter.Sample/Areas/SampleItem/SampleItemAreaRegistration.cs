using System.Web.Mvc;
using System.Web.Routing;

namespace Sitecore.ItemAdapter.Sample.Areas.SampleItem
{
    public class SampleItemAreaRegistration : AreaRegistration 
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            var sampleItemRoute = context.MapRoute(
                "SampleItem_default",
                "SampleItem/{controller}/{action}/{id}",
                new { @controller="Sample", @action = "Index", @id = UrlParameter.Optional } //, @scAreaRegistration=true
            );
            InitializeSitecoreAreaRoute(sampleItemRoute);
        }

        private static void InitializeSitecoreAreaRoute(Route route)
        {
            IRouteHandler routeHandler = route.RouteHandler;
            if (routeHandler != null && !(routeHandler is StopRoutingHandler))
            {
                route.RouteHandler = new Sitecore.Mvc.Routing.RouteHandlerWrapper(routeHandler);
            }
        }
        public override string AreaName
        {
            get
            {
                return "SampleItem";
            }
        }

    }
}