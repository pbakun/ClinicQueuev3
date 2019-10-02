using Microsoft.AspNetCore.SignalR;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BackgroundServices.Tasks;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.ServiceLogic;
using WebApp.Utility;

namespace WebApp.Hubs
{
    public class QueueHub : Hub
    {
        public static List<HubUser> _connectedUsers = new List<HubUser>();
        public static List<HubUser> _waitingUsers = new List<HubUser>();

        private readonly IRepositoryWrapper _repo;
        private readonly IQueueService _queueService;

        //hubContext for sending messages to clients from long-running processes (like Timer)
        private readonly IHubContext<QueueHub> _hubContext;

        private DoctorDisconnectedTimer _timer;

        public QueueHub(IRepositoryWrapper repo, IQueueService queueService, IHubContext<QueueHub> hubContext)
        {
            _repo = repo;
            _queueService = queueService;
            _hubContext = hubContext;

        }

        public async Task RegisterDoctor(string userId, int roomNo)
        {
            var newUser = new HubUser {
                Id = userId,
                ConnectionId = Context.ConnectionId,
                GroupName = roomNo.ToString()
            };

            //add something to add connecting user if this is the same as already registered but with different connectiondID
            var userInControl = _connectedUsers.Where(m => m.Id != null && m.GroupName == newUser.GroupName).FirstOrDefault();
            if(userInControl == null)
            {
                await Groups.AddToGroupAsync(newUser.ConnectionId, newUser.GroupName).ConfigureAwait(false);

                var user = _repo.User.FindByCondition(u => u.Id == userId).FirstOrDefault();

                string doctorFullName = QueueHelper.GetDoctorFullName(user);

                await Clients.Group(roomNo.ToString()).SendAsync("ReceiveDoctorFullName", userId, doctorFullName);

                var queue = _queueService.FindByUserId(userId);

                _queueService.SetQueueActive(queue);

                await Clients.Group(roomNo.ToString()).SendAsync("ReceiveQueueNo", userId, queue.QueueNoMessage);
                await Clients.Group(roomNo.ToString()).SendAsync("ReceiveAdditionalInfo", userId, queue.AdditionalMessage);

                if (!_connectedUsers.Contains(newUser))
                    _connectedUsers.Add(newUser);
            }
            else
            {
                if (!_waitingUsers.Contains(newUser))
                    _waitingUsers.Add(newUser);

                await Clients.Caller.SendAsync("NotifyQueueOccupied", StaticDetails.QueueOccupiedMessage);
            }
        }

        public async Task RegisterPatientView(int roomNo)
        {
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
            //if disconnecting user was not connected to any hub group (group was occupied when user was connecting)
            if(groupMember == null)
            {
                var member = _waitingUsers.Where(c => c.ConnectionId == Context.ConnectionId).FirstOrDefault();
                _waitingUsers.Remove(member);
                return base.OnDisconnectedAsync(exception);
            }

            int memberRoomNo = Convert.ToInt32(groupMember.GroupName);

            //if group member changed roomNo reload patient view
            if (groupMember.Id != null && !_queueService.CheckRoomSubordination(groupMember.Id, memberRoomNo))
            {
                _queueService.SetQueueInactive(groupMember.Id);
                Clients.Group(groupMember.GroupName).SendAsync("Refresh", groupMember.GroupName);
            }
            else if (groupMember.Id != null)
            {
                //if Doctor disconnected start timer and send necessery info to Patient View after
                _timer = new DoctorDisconnectedTimer(groupMember, SettingsHandler.ApplicationSettings.PatientViewNotificationAfterDoctorDisconnectedDelay);
                _timer.TimerFinished += Timer_TimerFinished;
            }
            
            _connectedUsers.Remove(groupMember);
            Groups.RemoveFromGroupAsync(connectionString, groupMember.GroupName);

            return base.OnDisconnectedAsync(exception);
        }

        private void Timer_TimerFinished(object sender, EventArgs e)
        {
            var groupMember = sender as HubUser;
            if (_connectedUsers.Where(i => i.Id == groupMember.Id).FirstOrDefault() == null)
            {
                _queueService.SetQueueInactive(groupMember.Id);
                _hubContext.Clients.Group(groupMember.GroupName).SendAsync("Refresh", groupMember.GroupName);
            }
            _timer.Dispose();
        }

        public async Task NewQueueNo(string userId, int queueNo, int roomNo)
        {
            var hubUser = _connectedUsers.Where(u => u.Id == userId).FirstOrDefault();
            if (hubUser != null)
            {
                Queue outputQueue = await _queueService.NewQueueNo(userId, queueNo);

                await Clients.Group(roomNo.ToString()).SendAsync("ReceiveQueueNo", userId, outputQueue.QueueNoMessage);
            }
        }

        public async Task NewAdditionalInfo(string userId, int roomNo, string message)
        {
            var hubUser = _connectedUsers.Where(u => u.Id == userId).FirstOrDefault();
            if (hubUser != null)
            {
                Queue outputQueue = await _queueService.NewAdditionalInfo(userId, message);

                await Clients.Group(roomNo.ToString()).SendAsync("ReceiveAdditionalInfo", userId, outputQueue.AdditionalMessage);
            }
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
