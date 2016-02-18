using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Infrastructure.Filters
{
    using System.Web.Mvc;

    public class ApplicationVersionHeaderFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add("Application name", "Live Auction");
            filterContext.HttpContext.Response.Headers.Add("Application version", "1.0.1");
            base.OnActionExecuted(filterContext);
        }
    }
}
