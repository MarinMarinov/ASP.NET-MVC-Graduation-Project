namespace Auction.Web.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class AuctionRoom: Hub
    {
        public void SendMessage(string message)
        {
            var msg = string.Format("{0}: {1}", Context.ConnectionId, message);
            this.Clients.All.addMessage(msg);
        }

        public void JoinRoom(string room)
        {
            this.Groups.Add(Context.ConnectionId, room);
            this.Clients.Caller.joinRoom(room);
        }

        public void SendMessageToRoom(string message, string[] rooms)
        {
            var msg = string.Format("{0}: {1}", Context.ConnectionId, message);

            for (int i = 0; i < rooms.Length; i++)
            {
                Clients.Group(rooms[i]).addMessage(msg);
            }
        }
    }
}