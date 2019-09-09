using AutoMapper;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.QueueService
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
        public void ChangeUserRoomNo(string userId, int newRoomNo)
        {
            throw new NotImplementedException();
        }

        public void NewAdditionalInfo(string userId, string message, int roomNo)
        {
            throw new NotImplementedException();
        }

        public void NewQueueNo(string userId, int queueNo, int roomNo)
        {
            throw new NotImplementedException();
        }

        public void ResetQueues()
        {
            throw new NotImplementedException();
        }
    }
}
