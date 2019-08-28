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
        public static readonly string dbFile = Path.Combine(Environment.CurrentDirectory, "AppData.db3");

    }
}
