using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.BackgroundServices.Tasks
{
    public class ResetQueue : ScheduledProcessor
    {
        protected override string Schedule => "*/1 * * * *";

        public ResetQueue(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Scheduled process fired!");
            return Task.CompletedTask;
        }
    }
}
