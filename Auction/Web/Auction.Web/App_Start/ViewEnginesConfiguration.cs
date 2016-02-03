namespace Auction.Web
{
    using System.Web.Mvc;

    public class ViewEnginesConfiguration
    {
        internal static void RegisterViewEngines()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}