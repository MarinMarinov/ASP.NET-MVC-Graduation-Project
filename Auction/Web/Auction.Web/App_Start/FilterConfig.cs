using System.Web;
using System.Web.Mvc;

namespace Auction.Web
{
    using Auction.Infrastructure.Filters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ApplicationVersionHeaderFilter());
        }
    }
}
