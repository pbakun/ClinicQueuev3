using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModel
{
    public class PatientViewModel
    {
        public string DoctorFullName { get; set; }
        public string QueueNoMessage { get; set; }
        public string QueueAdditionalInfo { get; set; }
        public int RoomNo { get; set; }
    }
}
