using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModel
{
    public class DoctorViewModel
    {
        public Queue Queue { get; set; }
        public List<int> AvailableRoomNo { get; set; }

        public DoctorViewModel()
        {
            AvailableRoomNo = new List<int> { 12, 13, 14};
        }
    }
}
