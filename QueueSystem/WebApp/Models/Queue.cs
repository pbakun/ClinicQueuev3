using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Queue
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int QueueNo { get; set; }
        public bool IsBreak { get; set; }
        public string AdditionalMessage { get; set; }
        public string OwnerInitials { get; set; }
        public int RoomNo { get; set; }
        public DateTime Timestamp { get; set; }

        private string queueNoMessage;
        public string QueueNoMessage
        {
            get
            {
                if (IsBreak)
                {
                    queueNoMessage = "Przerwa";
                }
                else
                {
                    queueNoMessage = String.Concat(OwnerInitials, QueueNo.ToString());
                }

                return queueNoMessage;
            }
        }
        public int UserId { get; set; }


    }
}
