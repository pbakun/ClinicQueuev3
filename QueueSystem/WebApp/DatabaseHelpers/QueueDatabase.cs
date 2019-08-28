using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.DatabaseHelpers
{
    public class QueueDatabase
    {
        public static void AddQueue(Queue queue)
        {
            using (SQLiteConnection conn = new SQLiteConnection(StaticDetails.dbFile))
            {
                //create table if there isn't any
                conn.CreateTable<Queue>();
                //check if there is a Queue with current's user id
                var data = conn.Table<Queue>().Where(u => u.UserId == queue.UserId).FirstOrDefault();
                if (data != null)
                {
                    data.OwnerInitials = queue.OwnerInitials;
                    data.Timestamp = queue.Timestamp;
                    data.RoomNo = queue.RoomNo;
                    DatabaseHelper.Update(data);
                }
                //if not create new Queue in db
                else
                {
                    DatabaseHelper.Insert(queue);
                }
            }

        }

        public static void UpdateUsersQueue(Queue queue)
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                //find current queue update db
                var data = conn.Table<Queue>().Where(u => u.UserId == queue.UserId);
                if (data != null)
                {
                    DatabaseHelper.Update(queue);
                }
            }
        }

        public static Queue FindQueue(int? userId)
        {
            Queue user = new Queue();
            if (userId != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
                {
                    conn.CreateTable<Queue>();
                    user = conn.Table<Queue>().Where(u => u.UserId == userId).FirstOrDefault();
                }
            }
            return user;
        }

        public static Queue FindQueueByRoomNo(int? roomNo)
        {
            Queue user = new Queue();
            if (roomNo != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
                {
                    conn.CreateTable<Queue>();
                    user = conn.Table<Queue>().Where(u => u.RoomNo == roomNo).OrderBy(t => t.Timestamp).FirstOrDefault();
                }
            }
            return user;
        }

        public static List<Queue> ReadDatabase()
        {
            List<Queue> user;
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {

                user = conn.Table<Queue>().ToList();
                //conn.Delete(user[1]);

            }
            return user;
        }
    }
}
