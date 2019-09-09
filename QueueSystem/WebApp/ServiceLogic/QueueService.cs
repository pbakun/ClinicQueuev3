using AutoMapper;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ServiceLogic
{
    public class QueueService : IQueueService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public QueueService(IRepositoryWrapper repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #region Implmenting interface

        public Queue ChangeUserRoomNo(string userId, int newRoomNo)
        {
            var user = _repo.User.FindByCondition(u => u.Id == userId).FirstOrDefault();

            user.RoomNo = newRoomNo;
            _repo.User.Update(user);
            var queue = _repo.Queue.FindByCondition(q => q.UserId == userId).FirstOrDefault();
            queue.RoomNo = newRoomNo;
            _repo.Queue.Update(queue);
            _repo.Save();

            var output = _mapper.Map<Queue>(queue);

            return output;
        }

        public Queue FindByRoomNo(int roomNo)
        {
            //returns queue with newest Timestamp
            var queue = _repo.Queue.FindByCondition(r => r.RoomNo == roomNo).OrderBy(t => t.Timestamp).FirstOrDefault();
            
            Queue output = _mapper.Map<Queue>(queue);

            return output;
        }

        public Queue FindByUserId(string userId)
        {
            var queue = _repo.Queue.FindByCondition(u => u.UserId == userId).FirstOrDefault();
            Queue output = new Queue();
            if (queue == null)
                output = CreateQueue(userId);
            else
            {
                queue.Timestamp = DateTime.UtcNow;
                _repo.Queue.Update(queue);
                _repo.Save();
                output = _mapper.Map<Queue>(queue);
            }

            return output;
        }

        public async Task<Queue> NewAdditionalInfo(string userId, string message)
        {
            var queue = _repo.Queue.FindByCondition(i => i.UserId == userId).FirstOrDefault();
            if (message.Length > 0)
                queue.AdditionalMessage = message;
            else queue.AdditionalMessage = string.Empty;

            queue.Timestamp = DateTime.UtcNow;

            _repo.Queue.Update(queue);
            await _repo.SaveAsync();

            Queue outputQueue = _mapper.Map<Queue>(queue);

            return outputQueue;
        }

        public async Task<Queue> NewQueueNo(string userId, int queueNo)
        {
            var queue = _repo.Queue.FindByCondition(i => i.UserId == userId).FirstOrDefault();

            //if queueNo == -1 (Break) // == -2 (SpecialNo)
            if (queueNo > 0)
            {
                queue.QueueNo = queueNo;
                queue.IsBreak = false;
                queue.IsSpecial = false;
            }
            else if (queueNo == -1 && queue.IsBreak == false)
            {
                queue.IsBreak = true;
            }
            else if (queueNo == -2 && queue.IsSpecial == false)
            {
                queue.IsSpecial = true;
            }
            else
            {
                queue.IsBreak = false;
                queue.IsSpecial = false;
            }

            queue.Timestamp = DateTime.UtcNow;
            _repo.Queue.Update(queue);
            await _repo.SaveAsync();

            Queue outputQueue = _mapper.Map<Queue>(queue);

            return outputQueue;
        }

        public Queue ResetQueues()
        {
            throw new NotImplementedException();
        }

        public Queue CreateQueue(string userId)
        {
            var user = _repo.User.FindByCondition(u => u.Id == userId).FirstOrDefault();
            Queue queue = new Queue
            {
                UserId = user.Id,
                RoomNo = user.RoomNo,
                QueueNo = 1,
                OwnerInitials = String.Concat(user.FirstName.First(), user.LastName.First()),
                Timestamp = DateTime.UtcNow
            };
            _repo.Queue.Add(_mapper.Map<Entities.Models.Queue>(queue));
            _repo.Save();

            return queue;
        }

        public bool CheckRoomSubordination(string userId, int roomNo)
        {
            var queue = _repo.Queue.FindByCondition(u => u.UserId == userId).FirstOrDefault();
            if (queue.RoomNo == roomNo)
                return true;

            return false;
        }
        #endregion

        #region Custom Private Methods


        #endregion
    }
}
