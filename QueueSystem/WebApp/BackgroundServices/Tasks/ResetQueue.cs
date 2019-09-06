using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Hubs;

namespace WebApp.BackgroundServices.Tasks
{
    public class ResetQueue : ScheduledProcessor
    {
        //Reset queue every day at 22:00
        protected override string Schedule => "0 22 * * *";

        private IHubContext<QueueHub> _hubContext;
        private readonly IServiceScopeFactory _scopeFactory;

        public ResetQueue(IServiceScopeFactory serviceScopeFactory, IHubContext<QueueHub> hubContext) : base(serviceScopeFactory)
        {
            _hubContext = hubContext;
            _scopeFactory = serviceScopeFactory;
        }

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var queues = db.Queue.ToList();

                foreach (var queue in queues)
                {
                    queue.QueueNo = 1;
                    _hubContext.Clients.Groups(queue.RoomNo.ToString()).SendAsync("ResetQueue", queue.QueueNoMessage);
                }
                db.SaveChanges();
            }

            //_hubContext.Clients.All.SendAsync("ResetQueue", "PB1");

            return Task.CompletedTask;
        }
    }
}
