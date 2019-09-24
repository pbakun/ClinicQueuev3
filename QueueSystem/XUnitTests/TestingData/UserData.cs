using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTests.TestingData
{
    public class UserData
    {
        public Entities.Models.User User { get; set; }

        private int _roomNo;

        public UserData()
        {
            User = new Entities.Models.User();
            SetDefaults();
        }

        private void SetDefaults()
        {
            
        }

        public UserData WithRoomNo(int roomNo)
        {
            _roomNo = roomNo;
            return this;
        }

        public Entities.Models.User Build()
        {
            User.RoomNo = _roomNo;
            return User;
        }

        public List<Entities.Models.User> BuildAsList()
        {
            Build();
            var output = new List<Entities.Models.User>();
            output.Add(User);
            return output;
        }
    }
}
