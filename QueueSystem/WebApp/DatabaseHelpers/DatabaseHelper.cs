using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Utility;

namespace WebApp.DatabaseHelpers
{
    public class DatabaseHelper
    {
        public static readonly string dbFile = StaticDetails.dbFile;


        public static bool Insert<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int numberOfRows = conn.Insert(item);
                if (numberOfRows > 0)
                {
                    result = true;
                }
                return result;
            }
        }

        public static bool Update<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int numberOfRows = connection.Update(item);
                if (numberOfRows > 0)
                    result = true;
            }

            return result;
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int numberOfRows = connection.Delete(item);
                if (numberOfRows > 0)
                    result = true;
            }

            return result;
        }

    }
}
