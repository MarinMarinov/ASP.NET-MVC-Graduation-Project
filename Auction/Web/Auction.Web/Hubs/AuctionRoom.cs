namespace Auction.Web.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Auction.Services.Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;

    public class AuctionRoom : Hub
    {
        private static Dictionary<string, string> usersDictionary = new Dictionary<string, string>();
        private IBidsServices bids;

        public AuctionRoom(IBidsServices bidService)
        {
            this.bids = bidService;
        }

        public void Send(string receiverId, int auctionId, int value)
        {
            var senderId = this.Context.User.Identity.GetUserId();
            var senderName = this.Context.User.Identity.Name;

            if (!this.IsValidInput(receiverId, value))
            {
                return;
            }

            var bid = this.bids.Create(value, senderId, auctionId, new List<string> { receiverId });

            if (usersDictionary.ContainsKey(receiverId))
            {
                var receiverClient = Clients.Client(usersDictionary[receiverId]);

                if (receiverClient != null)
                {
                    receiverClient.broadcastMessage(senderName, bid.Value, bid.CreatedOn);
                }
            }
            else
            {
                this.Clients.Client(this.Context.ConnectionId).broadcastMessage("Server", "User is not connected");
            }
        }

        // TODO: From where? The knowledge base 
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

            if (value < 1)
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