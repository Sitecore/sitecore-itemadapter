using System.Web.Mvc;

namespace Sitecore.ItemAdapter.Sample.Areas.SampleItem
{
    public class SampleItemAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SampleItem";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SampleItem_default",
                "SampleItem/{controller}/{action}/{id}",
                new { controller="Sitecore", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}