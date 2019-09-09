using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Utility;

namespace WebApp.Helpers
{
    public static class QueueHelper
    {
        public static string QueueNoMessage()
        {
            return string.Empty;
        }

        public static string GetDoctorFullName(User user)
        {
            return String.Concat(StaticDetails.DoctorNamePrefix, user.FirstName, " ", user.LastName);
        }
    }
}
