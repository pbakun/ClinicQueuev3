using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Hubs;
using WebApp.Models;
using WebApp.ServiceLogic;

namespace WebApp.BackgroundServices.Tasks
{
    public class ResetQueue : ScheduledProcessor
    {
        //Reset queue every day at 22:00
        protected override string Schedule => "0 22 * * *";

        private IHubContext<QueueHub> _hubContext;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        

        public ResetQueue(IServiceScopeFactory serviceScopeFactory, IHubContext<QueueHub> hubContext, IMapper mapper) : base(serviceScopeFactory)
        {
            _hubContext = hubContext;
            _scopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IRepositoryWrapper>();

                var queues = repo.Queue.FindAll();

                foreach (var queue in queues)
                {
                    queue.QueueNo = 1;
                    repo.Queue.Update(queue);
                    var outputQueue = _mapper.Map<Queue>(queue);
                    _hubContext.Clients.Groups(queue.RoomNo.ToString()).SendAsync("ResetQueue", outputQueue.QueueNoMessage);
                }
                //repo.Queue.UpdateList(queues);
                repo.Save();
                Console.WriteLine("Queues reseted to 1");
            }

            return Task.CompletedTask;
        }
    }
}
