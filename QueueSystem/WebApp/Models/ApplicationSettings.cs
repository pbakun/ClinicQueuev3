using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ApplicationSettings
    {
        private List<int> availableRooms;
        private string queueOccupiedMessage;
        private int patientViewNotificationAfterDoctorDisconnectedDelay;

        public List<int> AvailableRooms
        {
            get { return availableRooms; }
            set
            {
                availableRooms = value;
            }
        }
        public string QueueOccupiedMessage
        {
            get { return queueOccupiedMessage; }
            set
            {
                queueOccupiedMessage = value;
            }
        }
        public int PatientViewNotificationAfterDoctorDisconnectedDelay
        {
            get { return patientViewNotificationAfterDoctorDisconnectedDelay; }
            set
            {
                patientViewNotificationAfterDoctorDisconnectedDelay = value;
            }
        }

    }
}
