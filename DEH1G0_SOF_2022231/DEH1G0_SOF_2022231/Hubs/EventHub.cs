using Microsoft.AspNetCore.SignalR;

namespace DEH1G0_SOF_2022231.Hubs
{
    /// <summary>
    /// Class that handles the event hub functionality, it handles the connection and disconnection of clients
    /// </summary>
    public class EventHub : Hub
    {
        /// <summary>
        /// Triggered when a client connects to the hub
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Triggered when a client disconnects from the hub
        /// </summary>
        /// <param name="exception">The exception that caused the disconnection</param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }

}
