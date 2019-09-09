using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.ServiceLogic;
using WebApp.Utility;

namespace WebApp.Hubs
{
    public class QueueHub : Hub
    {
        private static List<HubUser> _connectedUsers = new List<HubUser>();

        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        private readonly IQueueService _queueService;

        public QueueHub(IRepositoryWrapper repo, IMapper mapper, IQueueService queueService)
        {
            _repo = repo;
            _mapper = mapper;
            _queueService = queueService;
        }
        public async Task RegisterDoctor(string userId, int roomNo)
        {
            var newUser = new HubUser {
                Id = userId,
                ConnectionId = Context.ConnectionId,
                GroupName = roomNo.ToString()
            };

            await Groups.AddToGroupAsync(newUser.ConnectionId, newUser.GroupName);

            var user = _repo.User.FindByCondition(u => u.Id == userId).FirstOrDefault();

            string doctorFullName = QueueHelper.GetDoctorFullName(user);

            await Clients.Group(roomNo.ToString()).SendAsync("ReceiveDoctorFullName", userId, doctorFullName);

            var queue = _queueService.FindByUserId(userId);

            await Clients.Group(roomNo.ToString()).SendAsync("ReceiveQueueNo", userId, queue.QueueNoMessage);
            await Clients.Group(roomNo.ToString()).SendAsync("ReceiveAdditionalInfo", userId, queue.AdditionalMessage);

            if (!_connectedUsers.Contains(newUser))
                _connectedUsers.Add(newUser);
        }

        public async Task RegisterPatientView(int roomNo)
        {   //todo
            var newUser = new HubUser
            {
                ConnectionId = Context.ConnectionId,
                GroupName = roomNo.ToString()
            };

            await Groups.AddToGroupAsync(newUser.ConnectionId, newUser.GroupName);

            if(!_connectedUsers.Contains(newUser))
                _connectedUsers.Add(newUser);

        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connectionString = Context.ConnectionId;
            var groupMember = _connectedUsers.Where(c => c.ConnectionId == Context.ConnectionId).FirstOrDefault();
            int memberRoomNo = Convert.ToInt32(groupMember.GroupName);
            
            //if group member changed roomNo reload patient view
            if(groupMember.Id != null && !_queueService.CheckRoomSubordination(groupMember.Id, memberRoomNo))
            {
                Clients.Group(groupMember.GroupName).SendAsync("Refresh", groupMember.GroupName);
            }
            
            _connectedUsers.Remove(groupMember);
            Groups.RemoveFromGroupAsync(connectionString, groupMember.GroupName);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewQueueNo(string userId, int queueNo, int roomNo)
        {
            Queue outputQueue = await _queueService.NewQueueNo(userId, queueNo);

            await Clients.Group(roomNo.ToString()).SendAsync("ReceiveQueueNo", userId, outputQueue.QueueNoMessage);
        }

        public async Task NewAdditionalInfo(string userId, int roomNo, string message)
        {
            Queue outputQueue = await _queueService.NewAdditionalInfo(userId, message);

            await Clients.Group(roomNo.ToString()).SendAsync("ReceiveAdditionalInfo", userId, outputQueue.AdditionalMessage);
        }
    }

    public class HubUser
    {
        public string Id { get; set; }
        public string ConnectionId { get; set; }
        public IClientProxy Client { get; set; }
        public string GroupName { get; set; }
    }
}
