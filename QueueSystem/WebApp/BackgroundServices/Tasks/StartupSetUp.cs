using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.ServiceLogic;

namespace WebApp.BackgroundServices.Tasks
{
    public class StartupSetUp : ScopedProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public StartupSetUp(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            SetAllQueuesInActive();
            this.StopAsync(new CancellationToken());
            return Task.CompletedTask;
        }

        private void SetAllQueuesInActive()
        {
            using(var scope = _serviceScopeFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IQueueService>();

                var queues = service.FindAll();

                foreach (var queue in queues)
                {
                    service.SetQueueInActive(queue.UserId);
                }

            }
            
        }

        
    }
}
