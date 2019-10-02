using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ServiceLogic
{
    public interface IQueueService
    {
        Task<Queue> NewQueueNo(string userId, int queueNo);
        Task<Queue> NewAdditionalInfo(string userId, string message);
        Queue ChangeUserRoomNo(string userId, int newRoomNo);
        Queue ResetQueues();
        Queue FindByUserId(string userId);
        Queue FindByRoomNo(int roomNo);
        List<Queue> FindAll();
        Queue CreateQueue(string userId);
        bool CheckRoomSubordination(string userId, int roomNo);
        void SetQueueActive(Entities.Models.Queue queueId);
        void SetQueueInactive(string userId);
    }
}
