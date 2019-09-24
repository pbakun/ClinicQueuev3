using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BackgroundServices.Tasks;

namespace WebApp.Models
{
    public class Queue : Entities.Models.Queue
    {
        private string _queueNoMessage;
        public string QueueNoMessage
        {
            get
            {
                if (IsBreak)
                {
                    _queueNoMessage = "Przerwa";
                }
                else if (IsSpecial && OwnerInitials.Length > 0)
                {
                    _queueNoMessage = String.Concat(OwnerInitials, QueueNo.ToString(), "A");
                }
                else if (OwnerInitials.Length > 0)
                {
                    _queueNoMessage = String.Concat(OwnerInitials, QueueNo.ToString());
                }
                else
                {
                    _queueNoMessage = SettingsHandler.ApplicationSettings.MessageWhenNoDoctorActiveInQueue;
                }

                return _queueNoMessage;
            }
        }

        public Queue() : base()
        {
            OwnerInitials = string.Empty;
        }

    }
}
