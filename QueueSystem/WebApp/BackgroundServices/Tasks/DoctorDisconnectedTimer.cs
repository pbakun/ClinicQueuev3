using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public class DoctorDisconnectedTimer : IDisposable
    {
        public System.Timers.Timer _timer;
        public event EventHandler TimerFinished;
        private string _groupName;
        private IHubContext<QueueHub> _hubContext;
        
        public DoctorDisconnectedTimer(string groupName, int delay)
        {
            _groupName = groupName;
            _timer = new System.Timers.Timer(delay);
            _timer.Start();
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();
            OnTimerFinish();
        }

        private void OnTimerFinish()
        {
            if (TimerFinished != null)
            {
                TimerFinished.Invoke(_groupName, new EventArgs());
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
