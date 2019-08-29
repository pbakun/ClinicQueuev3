using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public class QueueHub : Hub
    {
        public async Task NewQueueNo(int userId, int queueNo)
        {
            await Clients.All.SendAsync("ReceiveMessage", userId, queueNo);
        }
    }
}
