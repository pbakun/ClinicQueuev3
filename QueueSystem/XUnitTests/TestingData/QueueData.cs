using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTests.TestingData
{
    public class QueueData
    {
        public Entities.Models.Queue Queue { get; set; }

        private int _queueNo;
        private string _additionalMessage;
        private bool _isBreak;
        private bool _isSpecial;
        private bool _isActive;
        private string _ownerInitials;
        private int _roomNo;

        public QueueData()
        {
            Queue = new Entities.Models.Queue();
            SetDefaults();
        }

        private void SetDefaults()
        {
            Queue.OwnerInitials = string.Empty;
            Queue.AdditionalMessage = string.Empty;
            Queue.OwnerInitials = "PB";
        }

        public QueueData WithQueueNo(int queueNo)
        {
            _queueNo = queueNo;
            return this;
        }

        public QueueData WithMessage(string message)
        {
            _additionalMessage = message;
            return this;
        }

        public QueueData WithRoomNo(int roomNo)
        {
            _roomNo = roomNo;
            return this;
        }

        public QueueData WithBreak(bool isBreak)
        {
            _isBreak = isBreak;
            return this;
        }

        public QueueData WithSpecial(bool special)
        {
            _isSpecial = special;
            return this;
        }

        public QueueData WithOwnerInitials(string initials)
        {
            _ownerInitials = initials;
            return this;
        }

        public Entities.Models.Queue Build()
        {
            Queue.QueueNo = _queueNo;
            Queue.AdditionalMessage = _additionalMessage;
            Queue.IsBreak = _isBreak;
            Queue.IsSpecial = _isSpecial;
            Queue.IsActive = _isActive;
            Queue.RoomNo = _roomNo;

            return Queue;
        }

        public List<Entities.Models.Queue> BuildAsList()
        {
            Build();
            var output = new List<Entities.Models.Queue>();
            output.Add(Queue);

            return output;
        }

    }
}
