﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ApplicationSettings
    {
        public List<int> AvailableRooms { get; set; }
        public string QueueOccupiedMessage { get; set; }
        public int PatientViewNotificationAfterDoctorDisconnectedDelay { get; set; }
    }
}
