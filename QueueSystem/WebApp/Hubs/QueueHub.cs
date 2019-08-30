using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public class QueueHub : Hub
    {
        private List<HubUser> ConnectedUsers;
        public async Task Register(int userId, int roomNo)
        {
            var newUser = new HubUser {
                Id = userId,
                ConnectionId = Context.ConnectionId,
                Client = Clients.Caller
            };

            ConnectedUsers.Add(newUser);
        }

        public async Task RegisterPatientView(int roomNo)
        {   //todo
            var newUser = new HubUser
            {
                ConnectionId = Context.ConnectionId,
                Client = Clients.Caller
            };
            if(!ConnectedUsers.Contains(newUser))
                ConnectedUsers.Add(newUser);

        }

        public async Task NewQueueNo(int userId, int queueNo, int roomNo)
        {

            //Console.WriteLine(roomNo.ToString());
            //foreach (var user in ConnectedUsers)
            //{
            //    await Clients.Client(ConnectedUsers.Where(m => m.Id == userId).Select(s => s.ConnectionId).FirstOrDefault()).
            //        SendAsync("ReceiveQueueNo", userId, queueNo);
            //}

            //ConnectedUsers.Where(m => m.Id == userId).ToList().ForEach(u =>
            //    Clients.Client(u.ConnectionId).SendAsync("ReceiveQueueNo", userId, queueNo)
            //);
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
