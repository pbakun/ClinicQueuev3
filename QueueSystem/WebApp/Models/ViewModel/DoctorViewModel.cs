using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Utility;

namespace WebApp.Models.ViewModel
{
    public class DoctorViewModel
    {
        public Queue Queue { get; set; }
        public List<int> AvailableRoomNo { get; set; }

        public DoctorViewModel()
        {
            AvailableRoomNo = StaticDetails.AvailableRoomNo;
        }
    }
}
