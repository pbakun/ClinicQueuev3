using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public class QueueHub : Hub
    {
        public List<HubUser> ConnectedUsers { get; set; }
        public async Task Register(int userId, int roomNo)
        {
            var newUser = new HubUser {
                Id = userId,
                ConnectionId = Context.ConnectionId,
                Client = Clients.Caller
            };

            ConnectedUsers.Add(newUser);
        }

        public async Task NewQueueNo(int userId, int queueNo)
        {
            foreach(var user in ConnectedUsers)
            {
                await Clients.Client(ConnectedUsers.Where(m => m.Id == userId).Select(s => s.ConnectionId).FirstOrDefault()).
                    SendAsync("ReceiveQueueNo", userId, queueNo);
            }

            //ConnectedUsers.Where(m => m.Id == userId).ToList();
            //await Clients.All.SendAsync("ReceiveQueueNo", userId, queueNo);
        }
    }

    public class HubUser
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public IClientProxy Client { get; set; }
    }
}
