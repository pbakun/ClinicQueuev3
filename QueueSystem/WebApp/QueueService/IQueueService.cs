using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.QueueService
{
    public interface IQueueService
    {
        void NewQueueNo(string userId, int queueNo, int roomNo);
        void NewAdditionalInfo(string userId, string message, int roomNo);
        void ChangeUserRoomNo(string userId, int newRoomNo);
        void ResetQueues();

    }
}
