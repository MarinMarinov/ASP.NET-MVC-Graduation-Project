namespace Auction.Web.Hubs
{
    using Auction.Services.Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Auction.Data;
    using Auction.Models;

    public class AuctionRoom : Hub
    {
        private static Dictionary<string, string> usersDictionary = new Dictionary<string, string>();
        private IBidsServices bids;
        private AuctionDbContext db;

        public AuctionRoom(IBidsServices bidService)
        {
            this.bids = bidService;
            this.db = new AuctionDbContext();
        }

        public void Send(string receiverId, int auctionId, int value, int currentPrice, string winnerId)
        {
            var bidderId = this.Context.User.Identity.GetUserId();
            var bidderName = this.Context.User.Identity.Name;

            if (!this.IsValidInput(receiverId, value))
            {
                return;
            }

            // TODO Fix ACTIVE property settlement of Auction! Maybe the DB Context is different
            //bool isActive = this.bids.CheckIfAuctionIsActive(auctionId);

            var auction = this.db.Auctions.FirstOrDefault(a => a.Id == auctionId);
            bool isActive = auction.Active;

            if (!isActive)
            {
                Clients.All.broadcastMessage("Server", "Auction is not active");
            }
            else
            {
                if (winnerId == string.Empty)
                {
                    winnerId = null;
                }
                Bid bid = this.bids.Create(value, currentPrice, bidderId, winnerId, auctionId, new List<string> { receiverId });

                if (receiverId == "All")
                {
                    Clients.All.broadcastMessage(bid.CreatedOn, bidderName, bid.Value, bid.CurrentPrice, bid.WinnerUsername);
                }
                else
                {
                    if (usersDictionary.ContainsKey(receiverId))
                    {
                        var receiverClient = Clients.Client(usersDictionary[receiverId]);

                        if (receiverClient != null)
                        {
                            receiverClient.broadcastMessage(bid.CreatedOn, bidderName, bid.Value, bid.CurrentPrice, bid.WinnerUsername);
                        }
                    }
                    else
                    {
                        this.Clients.Client(this.Context.ConnectionId).broadcastMessage("Server", "User is not connected");
                    }
                }
            }
        }

        public override Task OnConnected()
        {
            if (this.Context.User.Identity.IsAuthenticated)
            {
                this.CacheUserContextToDictionary();
            }

            return base.OnConnected();
        }

        private bool IsValidInput(string receiverId, int value)
        {
            if (receiverId == null)
            {
                return false;
            }

            if (value < 0)
            {
                return false;
            }

            return true;
        }

        private void CacheUserContextToDictionary()
        {
            var userId = this.Context.User.Identity.GetUserId();

            if (!usersDictionary.ContainsKey(userId))
            {
                usersDictionary.Add(userId, this.Context.ConnectionId);
            }
            else
            {
                usersDictionary[userId] = this.Context.ConnectionId;
            }
        }
    }
}