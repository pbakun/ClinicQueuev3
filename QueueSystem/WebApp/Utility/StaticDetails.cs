using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Utility
{
    public static class StaticDetails
    {
        //database file directory
        //to be deleted (QueueDatabase.cs is using it for now)
        public static readonly string dbFile = Path.Combine(Environment.CurrentDirectory, "AppData.db3");

        public const string AdminUser = "Admin";
        public const string DoctorUser = "Doctor";
        public const string NurseUser = "Nurse";
        public const string PatientUser = "Patient";
    }
}
