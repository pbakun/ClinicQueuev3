using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.BackgroundServices
{
    public abstract class BackgroundService : IHostedService
    {

        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCancellationToken = new CancellationTokenSource();

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            //store executing task
            _executingTask = ExecuteAsync(_stoppingCancellationToken.Token);

            //if task completed return it, this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
                return _executingTask;

            //Otherwise task is running
            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            //stop called without start
            if (_executingTask == null)
                return;

            try
            {
                _stoppingCancellationToken.Cancel();
            }
            finally
            {
                //wait until the task is completed or stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        protected virtual async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            do
            {
                await Process();

                await Task.Delay(5000, cancellationToken);
            }
            while (!cancellationToken.IsCancellationRequested);
        }

        protected abstract Task Process();
    }
}
