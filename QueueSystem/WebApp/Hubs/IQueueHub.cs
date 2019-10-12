using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public interface IQueueHub
    {

        Task NewQueueNo(string userId, int queueNo, int roomNo);
    }
}
