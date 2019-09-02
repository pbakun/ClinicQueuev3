using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Hubs
{
    public class QueueHub : Hub
    {
        private static List<HubUser> _connectedUsers = new List<HubUser>();

        private readonly ApplicationDbContext _queueDb;

        public QueueHub(ApplicationDbContext queueDb)
        {
            _queueDb = queueDb;
        }
        public async Task RegisterDoctor(int userId, int roomNo)
        {
            var newUser = new HubUser {
                Id = userId,
                ConnectionId = Context.ConnectionId,
                GroupName = roomNo.ToString()
            };

            await Groups.AddToGroupAsync(newUser.ConnectionId, newUser.GroupName);

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

            //add some kind of erasing userId from his queue, delete queue Owner property, display desired info on PatientView

            _connectedUsers.Remove(groupMember);
            Groups.RemoveFromGroupAsync(connectionString, groupMember.GroupName);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewQueueNo(int userId, int queueNo, int roomNo)
        {
            var queue = _queueDb.Queue.Where(i => i.UserId == userId).FirstOrDefault();
            if(queueNo > 0)
            {
                queue.QueueNo = queueNo;
                queue.IsBreak = false;
            }
            else if(queueNo == -1 && queue.IsBreak == false)
            {
                queue.IsBreak = true;
            }
            else
            {
                queue.IsBreak = false;
            }
            
            queue.Timestamp = DateTime.UtcNow;
            await _queueDb.SaveChangesAsync();

            await Clients.Group(roomNo.ToString()).SendAsync("ReceiveQueueNo", userId, queue.QueueNoMessage);
        }

    }

    public class HubUser
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public IClientProxy Client { get; set; }

        public string GroupName { get; set; }
    }
}
