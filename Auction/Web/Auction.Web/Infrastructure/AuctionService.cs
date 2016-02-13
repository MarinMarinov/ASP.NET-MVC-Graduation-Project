namespace Auction.Web.Infrastructure
{
    using System.Diagnostics;

    public class AuctionService: IAuctionService
    {
        public void Work()
        {
            Trace.WriteLine("Autofac is injecting properly dependencies");
        }
    }
}